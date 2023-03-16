using AutomakerWorkEmail.Models;

namespace AutomakerWorkEmail.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPassword()
        {
            string password = "4";

            Worker actual;

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                actual = db.Workers.Where(p => p.Password == password).FirstOrDefault();
            }

            Assert.AreEqual(password, actual.Password);
        }

        [Test]
        public void TestLogin()
        {
            string login = "5";

            Worker actual;

            using (AutomakerWorkEmailContext db = new AutomakerWorkEmailContext())
            {
                actual = db.Workers.Where(p => p.Login == login).FirstOrDefault();
            }

            Assert.AreEqual(login, actual.Login);
        }
    }
}