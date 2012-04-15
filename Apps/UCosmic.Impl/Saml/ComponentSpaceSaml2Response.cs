using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml;
using ComponentSpace.SAML2;
using ComponentSpace.SAML2.Assertions;
using ComponentSpace.SAML2.Bindings;
using ComponentSpace.SAML2.Protocols;

namespace UCosmic
{
    public class ComponentSpaceSaml2Response : Saml2Response
    {
        private readonly SAMLResponse _samlResponse;
        private SAMLAssertion _samlAssertion;
        private NameID _subjectNameId;
        private Issuer _issuer;
        private RelayState _relayState;

        public ComponentSpaceSaml2Response(XmlElement responseElement, string relayStateId,
            Saml2SsoBinding spBinding, X509Certificate2 encryptionCertificate, HttpContextBase httpContext)
            : base(responseElement, relayStateId, spBinding, encryptionCertificate, httpContext)
        {
            _samlResponse = new SAMLResponse(ResponseElement);
            SAML.HttpContext = HttpContext;
        }

        public override bool IsSigned
        {
            get { return SAMLMessageSignature.IsSigned(ResponseElement); }
        }

        public override bool VerifySignature()
        {
            return IsSigned && SAMLMessageSignature.Verify(ResponseElement);
        }

        public override string SubjectNameIdentifier
        {
            get { return (SubjectNameId != null) ? SubjectNameId.NameIdentifier : null; }
        }

        public override string IssuerNameIdentifier
        {
            get { return (Issuer != null) ? Issuer.NameIdentifier : null; }
        }

        public override string RelayResourceUrl
        {
            get { return (RelayState != null) ? RelayState.ResourceURL : null; }
        }

        public override string GetAttributeValueByFriendlyName(SamlAttributeFriendlyName friendlyName)
        {
            var attributeValues = GetAttributeValuesByFriendlyName(friendlyName);
            return attributeValues != null
                ? attributeValues.FirstOrDefault()
                : null;
        }

        public override string[] GetAttributeValuesByFriendlyName(SamlAttributeFriendlyName friendlyName)
        {
            if (Assertion != null)
            {
                var attributeStatement = Assertion.Statements.OfType<AttributeStatement>().SingleOrDefault();
                if (attributeStatement != null)
                {
                    if (attributeStatement.GetUnencryptedAttributes().Count > 0)
                    {
                        var attributes = attributeStatement.GetUnencryptedAttributes();
                        var attribute = attributes.SingleOrDefault(a => a.FriendlyName != null &&
                            a.FriendlyName.Equals(friendlyName.AsString(), StringComparison.OrdinalIgnoreCase));
                        if (attribute != null && attribute.Values.Count > 0 && attribute.Values[0].Data != null)
                        {
                            return
                            (
                                from value in attribute.Values
                                where value != null && value.Data != null
                                select value.Data.ToString()
                            )
                            .ToArray();
                        }
                    }
                }
            }
            return null;
        }

        private SAMLAssertion Assertion
        {
            get
            {
                if (_samlAssertion == null && _samlResponse != null)
                {
                    if (_samlResponse.GetEncryptedAssertions().Count > 0)
                        _samlAssertion = _samlResponse.GetEncryptedAssertions().First()
                            .Decrypt(EncryptionCertificate.PrivateKey, null);

                    else if (_samlResponse.GetAssertions().Count > 0)
                        _samlAssertion = _samlResponse.GetAssertions().First();
                }
                return _samlAssertion;
            }
        }

        private Issuer Issuer
        {
            get
            {
                if (_issuer == null && Assertion != null && Assertion.Issuer != null)
                    _issuer = Assertion.Issuer;
                return _issuer;
            }
        }

        private NameID SubjectNameId
        {
            get
            {
                if (_subjectNameId == null && Assertion != null)
                {
                    if (Assertion.Subject.EncryptedID != null)
                        _subjectNameId = Assertion.Subject.EncryptedID
                            .Decrypt(EncryptionCertificate.PrivateKey, null);

                    else if (Assertion.Subject.NameID != null)
                        _subjectNameId = Assertion.Subject.NameID;
                }
                return _subjectNameId;
            }
        }

        private RelayState RelayState
        {
            get
            {
                if (_relayState == null && !string.IsNullOrWhiteSpace(RelayStateId))
                    _relayState = RelayStateCache.Remove(RelayStateId);
                return _relayState;
            }
        }

    }
}