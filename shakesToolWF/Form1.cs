using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;

namespace shakesToolWF
{
    public partial class Form1 : Form
    {
        private String password = "Guildwars963";
        readonly List<string> usernames;
        String link;
        String currentUser;
        List<string> bosses;
        List<double> chances;
        List<string> fights;

        IWebDriver driver;

        public Form1()
        {
            InitializeComponent();

            link = "https://sftools.mar21.eu/dungeons.html";
            usernames = new List<string>
            {
                "Hexega Serdidon@w59.sfgame.net",
                "Balage Uzsak@w10.sfgame.net",
                "Beelux Eunaru@s20.sfgame.hu",
                "Farolomester@w20.sfgame.net",
                "Farolomester@w58.sfgame.net",
                "Farolomester@s19.sfgame.hu",
                "Farolomester@w56.sfgame.net",
                "Farolomester@w57.sfgame.net",
                "Farolomester@w50.sfgame.net",
            };

            bosses = new List<string>();
            chances = new List<double>();

            SetButtonTextForCharacters();

            label1.ForeColor = Color.Red;
        }

        private void SetButtonTextForCharacters()
        {
            var i = 0;
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.Text = usernames[i];
                i++;
                Console.Write(i);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            // Defines the current character's login name
            currentUser = (sender as Button).Text.ToString();

            OpenBrowser();

            CloseTermsAndConditions();

            //Click Poll saved Characters
            driver.FindElement(By.Id("load-stats"))
                .Click();

            //Click Endpoint
            driver.FindElement(By.CssSelector("[class='ui fluid button vertical']"))
                .Click();

            //Fill Username
            driver.FindElement(By.Name("username"))
                .SendKeys(currentUser);

            //Fill Password
            driver.FindElement(By.Name("password"))
                .SendKeys(password);

            //Click Login
            driver.FindElement(By.CssSelector("[data-op=login]"))
                .Click();

            //Click on Import
            if (driver.FindElement(By.Id("endpoint-modal")).Displayed)
            {
                driver.FindElement(By.CssSelector("[data-op=import]"))
                    .Click();
            }

            //Hover on Character's name
            Actions action = new Actions(driver);
            var hover = driver.FindElement(By.XPath("//*[@id=\"stats-list\"]/div[1]/div[1]/span"));

            action.MoveToElement(hover).Perform();

            System.Threading.Thread.Sleep(1000);

            //Click on Hidden button (revealed after hover on character's name)
            driver.FindElement(By.XPath("//*[@id=\"stats-list\"]/div[1]/div[2]/span"))
                .Click();

            //Click on Run Simulate All button
            driver.FindElement(By.Id("sim-run-all"))
                .Click();

            GetWinChancesAgainstBosses();

            driver.Quit();
        }

        private void GetWinChancesAgainstBosses()
        {
            var table = driver.FindElement(By.CssSelector("#results-modal > div.content > div"));
            var data = table.FindElements(By.XPath("./child::*"));
            fights = new List<string>();
            foreach (var row in data)
            {
                if (row.Text == null || row.Text == "")
                {
                    continue;
                }

                string i = row.Text;
                string[] clear = i.Split(" - ");
                if (clear.Length > 0)
                {
                    string part = clear[0].Split('#')[0];
                    clear[0] = Regex.Replace(part, @"\r\n", " ").Trim();
                    var a = Regex.Match(clear[1], @"^[^0-9]*").Value;
                    var b = Regex.Match(clear[1], @"[0-9]*.[0-9]*%").Value;
                    clear[1] = Regex.Replace(a, @"\r\n", " ").Trim();
                    clear[1] = clear[1] + "," + b;
                }
                string result = clear[0] + "," + clear[1];

                fights.Add(result);
            }
        }

        private void OpenBrowser()
        {
            var DeviceDriver = ChromeDriverService.CreateDefaultService();
            DeviceDriver.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-infobars");
            driver = new ChromeDriver(DeviceDriver, options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(link);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void CloseTermsAndConditions()
        {
            if (!driver.FindElement(By.CssSelector("[class='ui basic tiny modal active']")).Displayed)
            {
                return;
            }
            else
            {
                var termsAndConditions = driver.FindElement(By.CssSelector("[class='ui green fluid button']"));
                termsAndConditions.Click();
                System.Threading.Thread.Sleep(1000);
                var continueTaC = driver.FindElement(By.CssSelector("[class='ui black fluid button']"));
                continueTaC.Click();
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResultClick(object sender, EventArgs e)
        {
            Result resultForm = new Result();
            resultForm.usernames = usernames;
            resultForm.fights = fights;
            resultForm.ShowDialog(this);
        }
    }
}