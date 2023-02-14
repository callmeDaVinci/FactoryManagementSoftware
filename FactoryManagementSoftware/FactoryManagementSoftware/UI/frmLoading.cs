
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace FactoryManagementSoftware.UI
{
    public partial class frmLoading : Form
    { 
        public frmLoading() 
        {
            InitializeComponent();

            // StartPosition was set to FormStartPosition.Manual in the properties window.
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 4;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 15;
            this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            this.Size = new Size(w, h);
        }

        private int sec = 0;

        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static frmLoading loadingForm;

        //private int loadingCourt = 1;
        
        //private string loadingDot = ".";

        private string loadingText = "Loading... ";

        private void progressBar1_Click(object sender, System.EventArgs e)
        {

        }

        static public void ShowLoadingScreen()
        {
            // Make sure it is only launched once.

            if (loadingForm != null)
                return;

            Thread thread = new Thread(new ThreadStart(ShowForm))
            {
                IsBackground = true
            };

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            //Form.Activate();

        }

        static private void ShowForm()
        {
            loadingForm = new frmLoading();
            Application.Run(loadingForm);
        }

        static public void CloseForm()
        {
            
            if (loadingForm != null)
            {
                if (!loadingForm.IsHandleCreated)
                {
                    loadingForm.CreateControl();
                    //Thread.Sleep(100);
                }

                loadingForm.Invoke(new CloseDelegate(CloseFormInternal));
            }
            
            
        }

        static private void CloseFormInternal()
        {
            if(loadingForm != null)
            {
                loadingForm.Close();
                //UneableTimer();
                loadingForm = null;
            }
            
        }

        private void UneableTimer()
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            string text = loadingText;
            //loadingCourt++;

            //if(loadingCourt > 6)
            //{
            //    loadingCourt = 1;
            //}

            //for(int i = 0; i < loadingCourt; i++)
            //{
            //    text += loadingDot;
            //}

            sec++;
            //lblRunningTime.Text = sec + " s";

            lblLoadingText.Text = text +"("+sec+"s)";
            
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
