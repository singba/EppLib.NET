﻿using EppLib.Entities;
using EppLib.Extensions.Fury;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EppLib.Tests
{
    [TestClass]
    public class FuryExtensionLocalTest
    {
        [TestMethod]
        [TestCategory("FuryExtension")]
        [DeploymentItem("TestData/FuryLoginCommand.xml")]
        public void FuryLogin()
        {
            string expected = File.ReadAllText("FuryLoginCommand.xml");

            var command = new Login("username", "password");
            command.Options = new Options { MLang = "en", MVersion = "1.0" };

            var services = new Services();

            services.ObjURIs.Add("urn:ietf:params:xml:ns:epp-1.0");
            services.ObjURIs.Add("urn:ietf:params:xml:ns:domain-1.0");
            services.ObjURIs.Add("urn:ietf:params:xml:ns:host-1.0");
            services.ObjURIs.Add("urn:ietf:params:xml:ns:contact-1.0");

            //Fury extension
            services.Extensions.Add("urn:ietf:params:xml:ns:fury-2.0");

            command.Services = services;

            var xml = command.ToXml().InnerXml;

            Assert.AreEqual(expected, xml);
        }


        [TestMethod]
        [TestCategory("FuryExtension")]
        [DeploymentItem("TestData/FuryDomainCreateCommand.xml")]
        public void FuryCreateDomainWithPrivacy()
        {
            string expected = File.ReadAllText("FuryDomainCreateCommand.xml");

            var command = new DomainCreate("mydomain.ca", "jd1234");
            command.Password = "2fooBAR";
            command.DomainContacts.Add(new DomainContact("sh8013", "admin"));
            command.DomainContacts.Add(new DomainContact("sh8013", "tech"));

            command.Extensions.Add(new FuryDomainCreateExtension(true));

            var xml = command.ToXml().InnerXml;

            Assert.AreEqual(expected, xml);
        }

        [TestMethod]
        [TestCategory("FuryExtension")]
        [DeploymentItem("TestData/FuryDomainUpdateCommand.xml")]
        public void FuryUpdateDomainWithPrivacy()
        {
            string expected = File.ReadAllText("FuryDomainUpdateCommand.xml");

            var command = new DomainUpdate("example.com");
            command.Password = "password2";
            command.Extensions.Add(new FuryDomainUpdateExtension(true));

            var xml = command.ToXml().InnerXml;

            Assert.AreEqual(expected, xml);
        }

        [TestMethod]
        [TestCategory("FuryExtension")]
        [DeploymentItem("TestData/FuryContactCreateCommand.xml")]
        public void FuryContactCreateWithPrivacy()
        {
            string expected = File.ReadAllText("FuryContactCreateCommand.xml");

            var registrantContact = new Contact("agreed6",
    "Test Contact1", "Test Organization",
    "Ottawa", "123 Main Street", "ON", "K1R 7S8", "CA",
    "jdoe@example.com",
    new Telephone { Value = "+1.6471114444", Extension = "333" },
    new Telephone { Value = "+1.6471114445" });

            var command = new ContactCreate(registrantContact);

            var furyExtension = new FuryContactCreateExtension("en", "CCT", null);

            command.Extensions.Add(furyExtension);

            var xml = command.ToXml().InnerXml;

            Assert.AreEqual(expected, xml);
        }


        [TestMethod]
        [TestCategory("FuryExtension")]
        [DeploymentItem("TestData/FuryContactUpdateCommand.xml")]
        public void FuryContactUpdateWithPrivacy()
        {
            string expected = File.ReadAllText("FuryContactUpdateCommand.xml");

            var command = new ContactUpdate("agreed2");

            //change contact email and language

            var registrantContact = new Contact("agreed6",
    "Test Contact1", "Test Organization",
    "Ottawa", "123 Main Street", "ON", "K1R 7S8", "CA",
    "jdoe@example.com",
    new Telephone { Value = "+1.6471114444", Extension = "333" },
    new Telephone { Value = "+1.6471114445" });

            var contactChange = new ContactChange(registrantContact);

            contactChange.Email = "noprops@domain.fr";

            command.ContactChange = contactChange;

            command.Extensions.Add(new FuryContactUpdateExtension ("fr","en"));

            var xml = command.ToXml().InnerXml;

            Assert.AreEqual(expected, xml);
        }
    }
}
