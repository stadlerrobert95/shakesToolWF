using shakesToolWF.Models;
using System.ComponentModel;

namespace shakesToolWF
{
    public partial class Result : Form
    {
        public List<string> usernames;
        public List<string> fights;
        List<Fight> bosses;

        public Result()
        {
            InitializeComponent();
            bosses = new List<Fight>();
        }

        private void Result_Load(object sender, EventArgs e)
        {
            ClarifyUsernames();
            foreach (var fight in fights)
            {
                string[] strings = fight.ToString().Split(',');
                bosses.Add(new Fight(strings[1], strings[0], strings[2]));
            }

            var bindings = new BindingList<Fight>(bosses);
            var source = new BindingSource(bindings, null);
            dataGridView2.DataSource = source;
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ClarifyUsernames()
        {
            List<string> newUsernames = new();
            foreach (string username in usernames)
            {
                newUsernames.Add(username.Split('.')[0]);
            }
            usernames = newUsernames;
        }

    }
}
