﻿using WebApplicationTemplate.BLL.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebApplicationTemplate.BLL;
using WebApplicationTemplate.Objects.Security;

namespace WebApplicationTemplate.Test
{
    /// <summary>
    ///Se trata de una clase de prueba para UserBLLTest y se pretende que
    ///contenga todas las pruebas unitarias UserBLLTest.
    ///</summary>
    [TestClass()]
    public class UserBLLTest 
    {
        /// <summary>
        ///Obtiene o establece el contexto de la prueba que proporciona
        ///la información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext { get; set; }


        #region Atributos de prueba adicionales
        // 
        //Puede utilizar los siguientes atributos adicionales mientras escribe sus pruebas:
        //
        //Use ClassInitialize para ejecutar código antes de ejecutar la primera prueba en la clase 
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize para ejecutar código antes de ejecutar cada prueba
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///Una prueba de SelectUserByUsername
        ///</summary>
        [TestMethod()]
        public void SelectUserByUsernameTest()
        {
            UserBLL target = new UserBLL(TestSecurity.CurrentSession); // TODO: Inicializar en un valor adecuado
            string Username = "Test"; // TODO: Inicializar en un valor adecuado
            
            User user = target.SelectUserByUsername(Username);

            if (user != null)
            {
                Assert.AreEqual(user.Username, Username);
            }
            else
            {
                Assert.Inconclusive("Missing initialize script");
            }
        }
    }
}
