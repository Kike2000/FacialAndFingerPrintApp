using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using Prj_Capa_Negocio;
using Prj_Capa_Datos;

namespace FacialRecognition
{
    public partial class Recognizer : Form
    {
        //initialize
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        //now initialize a list to save recognized names
        List<string> NamePersons = new List<string>();
        string name = null;
        int t, ContTrain, NumLabels;
        public int veces = 0;
        public Recognizer()
        {
            InitializeComponent();
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                //Load of previus trainned faces and labels for each image
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;

                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);//make list string 
                }
                //initialize all variable same as previous

            }
            catch (Exception e)
            {
                //MessageBox.Show("Este usuario no está re");
            }
        }

        private void Recognizer_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Cerrar_TabPers_Click(object sender, EventArgs e)
        {
            Application.Idle -= FrameGrabber;
            if (grabber != null)
            {
                this.grabber.Dispose();
                this.OnClosed(e);
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //link it to star form
            //Hi lets start face recognition 
            //import opencv libraries
            //now same start camera and capture face as describe in face detection then go to recognition so
            //Initialize the capture device
            grabber = new Capture();
            grabber.QueryFrame();
            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
            //add frame grabber same in detection lecture
            //copy code
        }
        void FrameGrabber(object sender, EventArgs e)
        {

            RN_Personal obj = new RN_Personal();
            RN_Asistencia objas = new RN_Asistencia();
            DataTable datoPersona = new DataTable();
            DataTable dataAsis = new DataTable();
            
            string NroIdPersona;
            int cont = 1;
            string turaFoto;

            int HoLimite = 20;//Dtp_Hora_Limite.Value.Hour;
            int MiLimite =  18;

            int horaCaptu = DateTime.Now.Hour;
            int minutoCaptu = DateTime.Now.Minute;
            NamePersons.Add("");
            //now detect no. of faces in scene
            label2.Text = "0";

            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
          face,
          1.3,
          10,
          Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
          new Size(20, 20));

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);
                //initialize result,t and gray if (trainingImages.ToArray().Length != 0)
                {
                    //termcriteria against each image to find a match with it perform different iterations
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                    //call class by creating object and pass parameters
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                         trainingImages.ToArray(),
                         labels.ToArray(),
                         1000,
                         ref termCrit);
                    //next step is to name find for recognize face
                    name = recognizer.Recognize(result);
                    //now show recognized person name so
                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.LightGreen));//initalize font for the name captured

                }
                NamePersons[t - 1] = name;
                NamePersons.Add("");
                //now we will check detected faces multiple or just one in next lecture uptill now we are done with recognition
                label2.Text = facesDetected[0].Length.ToString();
                try
                {
                    if (name != "")
                    {
                        veces += 1;
                        if (veces == 1)
                        {
                            try
                            {
                                datoPersona = obj.RN_Buscar_Personal_xValor(name.Trim());
                                if (datoPersona.Rows.Count <= 0)
                                {
                                    
                                }
                                else
                                {
                                    var dt = datoPersona.Rows[0];
                                    var Lbl_Idperso = Convert.ToString(dt["Id_Pernl"]);
                                    var lbl_IdAsis = RN_Utilitario.RN_NroDoc(3);
                                    turaFoto = Convert.ToString(dt["Foto"]);
                                    
                                    if (objas.RN_Checar_SiPersonal_YaMarco_Asistencia(Lbl_Idperso) == true)
                                    {
                                        MessageBox.Show("El sistema verificó, que el personal ya marcó su entrada");
                                        Application.Idle -= FrameGrabber;
                                        this.grabber.Dispose();
                                        this.OnClosed(e);
                                        return;
                                    }

                                    if (objas.RN_Checar_SiPersonal_YaMarco_Entrada(Lbl_Idperso.Trim()) == true)
                                    {
                                        dataAsis = objas.RN_Buscar_Asistencia_deEntrada(Lbl_Idperso);
                                        if (dataAsis.Rows.Count < 1) return;

                                        lbl_IdAsis = Convert.ToString(dataAsis.Rows[0]["Id_asis"]);
                                        var horaIngreso = Convert.ToDateTime(dataAsis.Rows[0]["HoIngreso"]);

                                        var horas = (Convert.ToDateTime(DateTime.Now.ToString("hh:mm:ss")) - horaIngreso).TotalHours;
                                        int b = (int)horas;
                                        objas.RN_Registrar_Salida_Personal(lbl_IdAsis, Lbl_Idperso, DateTime.Now.ToString("hh:mm:ss"), b);

                                        if (BD_Asistencia.salida == true)
                                        {
                                            MessageBox.Show("Salida Registrada");
                                        }
                                    }
                                    else
                                    {
                                        lbl_IdAsis = RN_Utilitario.RN_NroDoc(3);
                                        var fe = lbl_IdAsis.Substring(3);
                                        var fes = Convert.ToInt32(fe);
                                        var Asis = fes + 1;
                                        var Asis2 = "AS-000" + Asis.ToString();
                                        objas.RN_Registrar_Entrada_Personal(Asis2, Lbl_Idperso, DateTime.Now.ToString("hh:mm:ss"), 8, 0, "Tardanza Justificada");

                                        if (BD_Asistencia.entrada == true)
                                        {
                                            RN_Utilitario.RN_Actualiza_Tipo_Doc(3);
                                            MessageBox.Show("Entrada Registrada");
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Algo malo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            }                            
                            Application.Idle -= FrameGrabber;
                            this.grabber.Dispose();
                            this.OnClosed(e);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //this.OnTabStopChanged(e);
                }
            }
            imageBox1.Image = currentFrame;
            //load haarclassifier and previous save image in directory to find match

            //hi now perform face recognitione
            //first of all add eigen class to project
            //i will upload in resource section so you can have it

            //Check that trained faces are present to recognize face
            //Done now run and test your program
            //Done with this now i will upload complete face recognition sdk
            //hope you learn program and enjoyed it

        }

        private void button4_Click(object sender, EventArgs e)
        {
            StartScreen s = new StartScreen();
            s.Show();
            this.Hide();
        }


    }
}
