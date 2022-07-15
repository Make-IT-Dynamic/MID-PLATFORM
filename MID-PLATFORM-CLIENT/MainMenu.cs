namespace MID_PLATFORM_CLIENT
{
    public partial class MainMenu : Form
    {     

        public MainMenu()
        {
            InitializeComponent();

            LoginPopup login = new LoginPopup();
            login.MdiParent = this;
            login.Show();

        }
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginPopup.NoLogin();
        }

        private void TableStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoginPopup.token == null)
            {
                LoginPopup.NoLogin();
                return;
            }

            string option = sender.ToString();
            GetListWindow newlist = new GetListWindow(option);
            newlist.MdiParent = this;
            newlist.Show();
        }

        private void CreateStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}