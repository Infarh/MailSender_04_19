using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MailSender.lib.Entityes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailSender.lib.Services.InMemory;

namespace MailSender.lib.Tests.Services.InMemory
{
    [TestClass]
    public class RecipientsDataInMemoryTests
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext Context)
        {

        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext Context)
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestCleanup]
        public void TestFinalize()
        {

        }

        [ClassCleanup]
        public static void ClassFinalize()
        {

        }

        [AssemblyCleanup]
        public static void AssemblyFinalize()
        {

        }

        public RecipientsDataInMemoryTests()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Add_Method_AddNewItemInService()
        {
            // AAA - подход
            // Arange

            const string expected_name = "Test Recipient";
            const string expected_email = "Test email";
            var new_recipient = new Recipient
            {
                Name = expected_name,
                Email = expected_email
            };

            var service = new RecipientsDataInMemory();

            // Act
            var actual_id = service.Add(new_recipient);

            // Assert
            Assert.AreEqual(new_recipient.Id, actual_id);
            Assert.IsTrue(service.GetAll().Contains(new_recipient));

            if(service.GetById(new_recipient.Id) != new_recipient)
                throw new AssertFailedException("В сервисе под указанным идентификатором хранится неверная сущность");
        }

        [TestMethod]
        public void GetById_ReturnCorrectItem()
        {
            const string expected_name = "Test Recipient";
            const string expected_email = "Test email";
            var new_recipient = new Recipient
            {
                Name = expected_name,
                Email = expected_email
            };

            var service = new RecipientsDataInMemory();

            var max_id = service.GetAll().Max(recipient => recipient.Id);

            var actual_id = service.Add(new_recipient);

            var expected_id = max_id + 1;

            //Assert.AreEqual(expected_id, actual_id);
            //Assert.AreEqual(expected_id, new_recipient.Id);

            var actual_recipient = service.GetById(expected_id);

            Assert.AreEqual(new_recipient, actual_recipient);
        }

        [TestMethod, Timeout(150), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetById_Thrown_ArgumentOutOfRangeException_OnNegativeId()
        {
            const int id = -5;

            var service = new RecipientsDataInMemory();

            service.GetById(id);
        }
    }
}
