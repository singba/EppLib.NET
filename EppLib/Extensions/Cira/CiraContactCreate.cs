﻿// Copyright 2012 Code Maker Inc. (http://codemaker.net)
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System.Xml;
using EppLib.Entities;

namespace EppLib.Extensions.Cira
{
    public class CiraContactCreate : ContactCreate
    {
        public CiraContactCreate(Contact contact) : base(contact)
        {
        }

        public override XmlDocument ToXml()
        {
            var ciraExtension = new CiraCreateExtension
            {
                Language = Language,
                OriginatingIpAddress = OriginatingIpAddress,
                CprCategory = CprCategory,
                AgreementVersion = AgreementVersion,
                AggreementValue = AggreementValue,
                CreatedByResellerId = CreatedByResellerId
            };

			Extensions.Clear();
            Extensions.Add(ciraExtension);

            return base.ToXml();
        }

        public string CreatedByResellerId { get; set; }

        public string AggreementValue { get; set; }

        public string AgreementVersion { get; set; }

        public string CprCategory { get; set; }

        public string OriginatingIpAddress { get; set; }

        public string Language { get; set; }
    }
}