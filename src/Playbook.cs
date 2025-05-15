using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrustedUninstaller.Shared
{
    [Serializable]
    public class VersionNumber : IXmlSerializable
    {
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(this.ToString());
        }

        public void ReadXml(XmlReader reader)
        {
            var text = (string)reader.ReadElementContentAs(typeof(string), null);
            if (!String.IsNullOrEmpty(text))
            {
                var version = GetVersionNumber(text);
                this.Major = version.Major;
                this.Minor = version.Minor;
                this.Revision = version.Revision;
            }
        }

        public XmlSchema GetSchema() => null;
        
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Revision { get; set; }
        
        public static bool operator ==(VersionNumber a, VersionNumber b)
        {
            if (a is null || b is null) return true;
            return a.IsEqual(b);
        }
        
        public static bool operator !=(VersionNumber a, VersionNumber b)
        {
            if (a is null || b is null) return false;
            return !a.IsEqual(b);
        }
        public static bool operator >=(VersionNumber a, VersionNumber b)
        {
            if (a is null || b is null) throw new ArgumentNullException();
            return a.IsGreaterThanOrEqualTo(b);
        }
        public static bool operator >(VersionNumber a, VersionNumber b)
        {
            if (a is null || b is null) throw new ArgumentNullException();
            return a.IsGreaterThan(b);
        } 

        public static bool operator <=(VersionNumber a, VersionNumber b)
        {
            if (a is null || b is null) throw new ArgumentNullException();
            return a.IsLessThanOrEqualTo(b);
        }
        public static bool operator <(VersionNumber a, VersionNumber b)
        {
            if (a is null || b is null) throw new ArgumentNullException();
            return a.IsLessThan(b);
        }
            
        public bool IsEqual(VersionNumber other) => Major == other.Major && Minor == other.Minor && Revision == other.Revision;
        public bool IsGreaterThan(VersionNumber other) => Major > other.Major || (Major >= other.Major && Minor > other.Minor) || (Major >= other.Major && Minor >= other.Minor && Revision > other.Revision);
        public bool IsLessThan(VersionNumber other) => other.IsGreaterThan(this);
        
        public bool IsGreaterThanOrEqualTo(VersionNumber other) => IsGreaterThan(other) || IsEqual(other);
        public bool IsLessThanOrEqualTo(VersionNumber other) => IsLessThan(other) || IsEqual(other);
        
        public override bool Equals(object obj)
        {
            if (obj is VersionNumber other)
                return this == other;
            return false;
        }

        public override string ToString() => Major + "." + Minor + "." + Revision;

        public static VersionNumber GetVersionNumber(string toBeParsed)
        {
            // Examples:
            // 0.4
            // 0.4 Alpha
            // 1.0.5
            // 1.0.5 Beta
            
            VersionNumber number = new VersionNumber();
            
            // Remove characters after first space (and the space itself)
            if (toBeParsed.IndexOf(' ') >= 0)
                toBeParsed = toBeParsed.Substring(0, toBeParsed.IndexOf(' '));

            var numbers = toBeParsed.Split('.');
            if (numbers.Length <= 0)
                throw new XmlException($"Invalid version number '{toBeParsed}'");
            for (var i = 0; i < numbers.Length; i++)
            {
                if (i == 0)
                    number.Major = int.Parse(numbers[i], CultureInfo.InvariantCulture);
                if (i == 1)
                    number.Minor = int.Parse(numbers[i], CultureInfo.InvariantCulture);
                if (i == 2)
                    number.Revision = int.Parse(numbers[i], CultureInfo.InvariantCulture);

                if (i > 2)
                    throw new Exception("Version number invalid.");
            }
            return number;
        }
    }
    
    public enum ErrorLevel
    {
        Success = 0,
        Error = 1,
        FatalError = 2,
    }
    [Serializable]
    public enum Requirement
    {
        [XmlEnum("Internet")]
        Internet = 0,
        [XmlEnum("NoInternet")]
        NoInternet = 1,
        [XmlEnum("DefenderDisabled")]
        DefenderDisabled = 2,
        [XmlEnum("DefenderToggled")]
        DefenderToggled = 3,
        [XmlEnum("NoPendingUpdates")]
        NoPendingUpdates = 4,
        [XmlEnum("Activation")]
        Activation = 5,
        [XmlEnum("NoAntivirus")]
        NoAntivirus = 6,
        [XmlEnum("LocalAccounts")]
        LocalAccounts = 11,
        [XmlEnum("PasswordSet")]
        PasswordSet = 11,
        [XmlEnum("AdministratorPasswordSet")]
        AdministratorPasswordSet = 8,
        [XmlEnum("PluggedIn")]
        PluggedIn = 9,
        [XmlEnum("NoTweakware")]
        NoTweakware = 10,
        [XmlEnum("FreshInstall")]
        FreshInstall = 12,
        [XmlEnum("UCPDDisabled")]
        UCPDDisabled = 13,
    } 
    public class Playbook : XmlDeserializable
    {
        [CanBeNull]
        public ISOSettings ISO { get; set; } = null;
        [CanBeNull]
        public OOBESettings OOBE { get; set; } = null;
        
        [XmlRequired(false)]
        public string Name { get; set; }
        [XmlRequired(false)]
        public string ShortDescription { get; set; }
        [XmlRequired(false)]
        public string Description { get; set; }
        
        [XmlRequired(false)]
        public string Title { get; set; }
        [XmlRequired(false)]
        public string Username { get; set; }
        public string Details { get; set; }
        [XmlRequired(false)]
        public string Version { get; set; }
        
        [XmlArray]
        [XmlArrayItem(Type = typeof(CheckboxPage))]
        [XmlArrayItem(Type = typeof(RadioPage))]
        [XmlArrayItem(Type = typeof(RadioImagePage))]
        public FeaturePage[] FeaturePages { get; set; }
        public Package[] Software { get; set; } = Array.Empty<Package>();

        public string ProgressText { get; set; } = "Deploying the selected Playbook configuration onto the system.";
        public int EstimatedMinutes { get; set; } = 25;
        
#nullable enable
        public string[]? SupportedBuilds { get; set; }
        public Requirement[] Requirements { get; set; } = new Requirement[] {};
        public string? InstallGuide { get; set; }
        public string? Git { get; set; }
        public string? DonateLink { get; set; }
        public string? Website { get; set; }
        public string? ProductCode { get; set; }
        public string? PasswordReplace { get; set; }
        public Guid? UniqueId { get; set; }
        [XmlAllowInlineArrayItem]
        public string[]? UpgradableFrom { get; set; }
        public bool? AllowUnsupportedUpgrades { get; set; } = true;
#nullable disable
        public bool Overhaul { get; set; } = false;
        public bool SupportsISO { get; set; } = false;

        [XmlIgnore]
        public string Path { get; set; }
        
        public bool? UseKernelDriver { get; set; } = null;

        [XmlIgnore]
        public List<string> Options { get; set; } = null;
        
        
        // Used for applied Playbooks
        [XmlIgnore]
        public ErrorLevel ErrorLevel = 0;
        [XmlIgnore]
        public string[] SelectedOptions = new string[] {};
        [XmlIgnore]
        public string[] AvailableOptions = new string[] {};
        [XmlIgnore]
        public DateTime AppliedTimeUTC = new DateTime();
        [XmlIgnore] [CanBeNull]
        public byte[] ImageBytes = null;
        

        [NotNull]
        public static Playbook[] GetAppliedPlaybooks()
        {
            var list = new List<Playbook>();
            using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\AME\Playbooks\Applied");
            if (key != null)
            {
                foreach (var subKeyName in key.GetSubKeyNames())
                {
                    try
                    {
                        var guid = subKeyName.Trim(new[] { '{', '}' });
                        if (Guid.TryParse(guid, out Guid uniqueId))
                        {
                            using var subKey = key.OpenSubKey(subKeyName)!;

                            var result = new Playbook();
                            result.UniqueId = uniqueId;
                            result.Version = (string)subKey.GetValue("Version");
                            result.Name = (string)subKey.GetValue("Name");
                            result.Username = (string)subKey.GetValue("Username");
                            result.Overhaul = unchecked((int)Convert.ToUInt32(subKey.GetValue("Overhaul"))) == 1;

                            result.ErrorLevel = (ErrorLevel)unchecked((int)Convert.ToUInt32(subKey.GetValue("ErrorLevel")));
                            result.AvailableOptions = (string[])subKey.GetValue("AvailableOptions");
                            result.SelectedOptions = (string[])subKey.GetValue("SelectedOptions");
                            result.AppliedTimeUTC = DateTime.FromBinary((long)subKey.GetValue("AppliedTimeUTC"));
                            
                            result.ImageBytes = (byte[])subKey.GetValue("Image");
                            list.Add(result);
                        }
                    }
                    catch (Exception e) { }
                }
            }

            var appliedDir = Environment.ExpandEnvironmentVariables(@"%ProgramData%\AME\AppliedPlaybooks");
            if (Directory.Exists(appliedDir))
            {
                foreach (var appliedPB in Directory.GetDirectories(appliedDir).Reverse())
                {
                    try
                    {
                        var result = DeserializePlaybook(appliedPB);

                        if (File.Exists(System.IO.Path.Combine(appliedPB, "playbook.png")) && File.Exists(System.IO.Path.Combine(appliedPB, "verified.txt")))
                            result.ImageBytes = File.ReadAllBytes(System.IO.Path.Combine(appliedPB, "playbook.png"));
                        if (File.Exists(System.IO.Path.Combine(appliedPB, "errors.txt")))
                            result.ErrorLevel = ErrorLevel.Error;

                        result.Path = appliedPB;
                        list.Add(result);
                    }
                    catch { }
                }
            }
            return list.ToArray();
        }

        public static Playbook DeserializePlaybook(string dir)
        {
            Playbook pb;
            
            XmlSerializer serializer = new XmlSerializer(typeof(Playbook));
            /*serializer.UnknownElement += delegate(object sender, XmlElementEventArgs args)
            {
                MessageBox.Show(args.Element.Name);
            };
            serializer.UnknownAttribute += delegate(object sender, XmlAttributeEventArgs args)
            {
                MessageBox.Show(args.Attr.Name);
            };*/
            try
            {
                using (XmlReader reader = XmlReader.Create($"{dir}\\playbook.conf"))
                {
                    pb = (Playbook)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException e)
            {
                if (e.InnerException == null)
                    throw;

                throw new XmlException(e.Message.TrimEnd('.') + ": " + e.InnerException.Message);
            }

            pb.Path = dir;
            return pb;
        }
        
        public VersionNumber GetVersionNumber()
        {
            return VersionNumber.GetVersionNumber(Version);
        }

        public class CheckboxPage : FeaturePage
        {
            public override void Validate()
            {
                if (Options.Length > 2 && TopLine != null && BottomLine != null)
                    throw new Exception(@$"CheckboxPage with a TopLine and BottomLine must not have more than 2 options.");
                if (Options.Length > 3 && (TopLine != null || BottomLine != null))
                    throw new Exception(@$"CheckboxPage with a TopLine or BottomLine must not have more than 3 options.");
                if (Options.Length > 4)
                    throw new Exception(@$"CheckboxPage must not have more than 4 options.");
                
                if (Options.Distinct().Count() != Options.Length)
                    throw new XmlException("Duplicate options found in CheckboxPage.");
            }
            
            public class CheckboxOption : Option
            {
                [XmlAttribute]
                public bool IsChecked { get; set; } = true;
                [XmlAttribute]
                public bool IsEnabled { get; set; } = true;
            }

            [XmlArray]
            [XmlArrayItem(ElementName = "CheckboxOption", Type = typeof(CheckboxOption))]
            public override Option[] Options { get; set; }
        }
        public class RadioPage : FeaturePage
        {
            public override void Validate()
            {
                if (Options.Length > 2 && TopLine != null && BottomLine != null)
                    throw new XmlException(@$"RadioPage with a TopLine and BottomLine must not have more than 2 options.");
                if (Options.Length > 3 && (TopLine != null || BottomLine != null))
                    throw new XmlException(@$"RadioPage with a TopLine or BottomLine must not have more than 3 options.");
                if (Options.Length > 4)
                    throw new XmlException(@$"RadioPage must not have more than 4 options.");
                    
                if (DefaultOption != null && !Options.Any(x => x.Name == DefaultOption))
                    throw new XmlException(@$"No option matching DefaultOption {DefaultOption} in Radio");
                
                if (Options.Distinct().Count() != Options.Length)
                    throw new XmlException("Duplicate options found in RadioPage.");
            }

            [XmlAttribute]
            public string DefaultOption { get; set; } = null;
            public class RadioOption : Option
            {
            }

            [XmlArray]
            [XmlArrayItem(ElementName = "RadioOption", Type = typeof(RadioOption))]
            public override Option[] Options { get; set; }
            [CanBeNull] public override string[] OptionNames() => Options?.Select(x => x.Name).ToArray();
        }
        public class RadioImagePage : FeaturePage
        {
            public override void Validate()
            {
                if (Options.Length > 4)
                    throw new XmlException(@$"RadioImagePage must not have more than 4 options.");
                
                if (DefaultOption != null && !Options.Any(x => x.Name == DefaultOption))
                    throw new XmlException(@$"No option matching DefaultOption {DefaultOption} in RadioImagePage.");

                if (Options.Distinct().Count() != Options.Length)
                    throw new XmlException("Duplicate options found in RadioImagePage.");

                if (Options.OfType<RadioImageOption>().Any(x => x.GradientTopColor == x.GradientBottomColor && !x.None))
                    throw new XmlException("RadioImageOption gradient colors must not be the same.");
                if (Options.OfType<RadioImageOption>().Any(x => !x.None && (x.GradientTopColor.Length != 7 || x.GradientBottomColor.Length != 7)))
                    throw new XmlException("RadioImageOption gradient colors must be in the format #RRGGBB.");
                if (Options.OfType<RadioImageOption>().Any(x => string.Equals(x.GradientTopColor, "#FFFFFF", StringComparison.OrdinalIgnoreCase) || string.Equals(x.GradientBottomColor, "#FFFFFF", StringComparison.OrdinalIgnoreCase) || string.Equals(x.GradientTopColor, "#000000", StringComparison.OrdinalIgnoreCase) || string.Equals(x.GradientBottomColor, "#000000", StringComparison.OrdinalIgnoreCase)))
                    throw new XmlException("RadioImageOption gradient colors must not be black or white.");
            }

            [XmlAttribute]
            public string DefaultOption { get; set; } = null;
            public class RadioImageOption : Option
            {
                public string FileName { get; set; } = null;

                public bool Fill { get; set; } = false;
                [XmlAttribute]
                public bool None { get; set; } = false;

                public string GradientTopColor { get; set; } = null;
                public string GradientBottomColor { get; set; } = null;
            }
            
            [XmlArray]
            [XmlArrayItem(Type = typeof(RadioImageOption))]
            public override Option[] Options { get; set; }
            [CanBeNull] public override string[] OptionNames() => Options?.Select(x => x.Name).ToArray();

            [XmlAttribute]
            public bool CheckDefaultBrowser { get; set; } = false;
        }
        
        public abstract class FeaturePage : XmlDeserializable
        {
            public override void Validate() => throw new Exception("FeaturePage cannot be used directly. Use CheckboxPage, RadioPage, or RadioImagePage instead.");

            [XmlAttribute]
            public string DependsOn { get; set; } = null;
            [XmlAttribute]
            public bool IsRequired { get; set; } = false;
            public Line TopLine { get; set; } = null;
            public Line BottomLine { get; set; } = null;
            
            public class Option
            {
                public string Name { get; set; } = null;
                public virtual string Text { get; set; }
                
                [XmlAttribute]
                public string DependsOn { get; set; } = null;
            }
            public class Line
            {
                [XmlAttribute("Text")]
                public string Text { get; set; }
                [XmlAttribute("Link")]
                public string Link { get; set; } = null;
            }
            
            [XmlArray]
            [XmlArrayItem(Type = typeof(Option))]
            public virtual Option[] Options { get; set; }
            [CanBeNull] public virtual string[] OptionNames() => Options?.Select(x => x.Name).ToArray();
            
            [XmlAttribute]
            public string Description { get; set; }
        }
    
        public class Package : XmlDeserializable
        {
            public override void Validate()
            {
                if (string.IsNullOrWhiteSpace(Name))
                    throw new XmlException("Software must have a Name.");
                if (string.IsNullOrWhiteSpace(Title))
                    throw new XmlException("Software must have a Title.");
                if (string.IsNullOrWhiteSpace(Description))
                    throw new XmlException("Software must have a Description.");
                if (string.IsNullOrWhiteSpace(Icon))
                    throw new XmlException("Software must have an Icon specified.");
            }

            [XmlAttribute]
            public string Option { get; set; } = null;
            [XmlAttribute]
            public bool Local { get; set; } = true;
            [XmlAttribute]
            public bool? DefaultWebBrowser { get; set; } = null;
            
            public string Name { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }
    }
    public enum ComponentIcon
    {
        Rocket,
        Privacy,
        Lock,
    }

    public class ISOSettings
    {
        public bool DisableBitLocker { get; set; } = false;
        public bool DisableHardwareRequirements { get; set; } = false;
    }

    public class OOBESettings
    {
        public OOBE.InternetRequirementLevel? Internet { get; set; } = null;
        public List<BulletPoint> BulletPoints { get; set; } = null;
    }
    public class BulletPoint
    {
        [XmlAttribute]
        public ComponentIcon Icon { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
    }
    
        public abstract class XmlDeserializable : IXmlSerializable
    {
        public class XmlAllowInlineArrayItemAttribute : Attribute { }

        public class XmlRequiredAttribute : Attribute
        {
            public bool AllowEmptyString { get; set; }
            public XmlRequiredAttribute() => AllowEmptyString = true;
            public XmlRequiredAttribute(bool allowEmptyString) => AllowEmptyString = allowEmptyString;
        }
        
        [Obsolete("XmlDeserializable does not support XmlInclude.")]
        public class XmlIncludeAttribute : Attribute { }

        /// <summary>
        /// This method gets called upon after deserialization, but before Deserialize returns. This is a good place to throw exceptions to the Deserialize call.
        /// </summary>
        public virtual void Validate() { }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var classType = reader.Name;
            List<string> assignedProperties = new List<string>();
            if (reader.HasAttributes)
            {
                while (reader.MoveToNextAttribute())
                {
                    var property = properties.FirstOrDefault(x => x.GetCustomAttribute<XmlIgnoreAttribute>() == null && (x.GetCustomAttributes<XmlAttributeAttribute>().FirstOrDefault(attr => !string.IsNullOrEmpty(attr.AttributeName) && attr.AttributeName == reader.Name) != null || (x.Name  == reader.Name && x.GetCustomAttribute<XmlAttributeAttribute>() != null))) ??
                        throw new XmlException($"Unrecognized attribute '{reader.Name}'");

                    if (assignedProperties.Contains(property.Name))
                        throw new XmlException($"Duplicate assignment for property '{property.Name}'");
                    assignedProperties.Add(property.Name);

                    property.SetValue(this, ReadContentValue(reader, property.PropertyType));
                }
            }

            while (true)
            {
                if (!MoveToNextElement(reader, classType))
                {
                    var requiredProperty = properties.FirstOrDefault(x =>
                        x.GetCustomAttribute<XmlIgnoreAttribute>() == null && x.GetCustomAttribute<XmlRequiredAttribute>() != null && (!assignedProperties.Contains(x.Name) || (x.PropertyType == typeof(string) && !x.GetCustomAttribute<XmlRequiredAttribute>().AllowEmptyString && string.IsNullOrWhiteSpace((string)x.GetValue(this)))));
                    if (requiredProperty != null && !assignedProperties.Contains(requiredProperty.Name)) 
                        throw new XmlException($"Required property '{requiredProperty.Name}' must be set.");
                    else if (requiredProperty != null)
                        throw new XmlException($"Property '{requiredProperty.Name}' must not be empty.");
                    
                    Validate();
                    return;
                }

                var property = properties.FirstOrDefault(x => x.GetCustomAttribute<XmlIgnoreAttribute>() == null && (x.GetCustomAttributes<XmlElementAttribute>().FirstOrDefault(attr => !string.IsNullOrEmpty(attr.ElementName) && attr.ElementName == reader.Name) != null || x.Name  == reader.Name)) ?? throw new XmlException($"Unrecognized element '{reader.Name}'");
                var elementName = string.IsNullOrEmpty(property.GetCustomAttribute<XmlElementAttribute>()?.ElementName) ? property.Name : property.GetCustomAttribute<XmlElementAttribute>()?.ElementName;
                
                if (!property.PropertyType.IsClass && reader.HasAttributes)
                    throw new XmlException($"Unexpected attributes found on XML element '{elementName}'");
                if (property.GetCustomAttribute<XmlAttributeAttribute>() != null && property.GetCustomAttribute<XmlElementAttribute>() == null)
                    throw new XmlException($"Property '{property.Name}' must be assigned as an XML attribute.");

                if (assignedProperties.Contains(property.Name))
                    throw new XmlException($"Duplicate assignment for property '{property.Name}'");
                assignedProperties.Add(property.Name);

                if (property.PropertyType.IsArray)
                {
                    IList arrayList = null;
                    var arrayItemType = property.PropertyType.GetElementType()!;
                    var arrayItemTypes = property.GetCustomAttributes(typeof(XmlArrayItemAttribute)).OfType<XmlArrayItemAttribute>().Select(x => x.Type).ToList();
                    arrayItemTypes.Add(arrayItemType);

                    reader.Read();
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        if (property.GetCustomAttribute<XmlAllowInlineArrayItemAttribute>() == null)
                            throw new XmlException($"Element '{elementName}' must be an array, not a single value.");

                        arrayList = Array.CreateInstance(arrayItemType, 1);
                        arrayList[0] = ReadContentValue(reader, arrayItemType);
                        property.SetValue(this, arrayList);
                    }

                    while (true)
                    {
                        if (!MoveToNextElement(reader, elementName))
                            break;

                        if (arrayList == null)
                            arrayList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new[] { property.PropertyType.GetElementType() }));

                        arrayList.Add(ReadElementValue(reader, arrayItemTypes));
                    }
                    if (arrayList != null)
                    {
                        Array array = Array.CreateInstance(arrayItemType, arrayList.Count);
                        arrayList.CopyTo(array, 0);
                        property.SetValue(this, array);
                    }
                }
                else
                    property.SetValue(this, ReadElementValue(reader, property.PropertyType));
            }
        }
        public void WriteXml(XmlWriter writer) => new XmlSerializer(this.GetType()).Serialize(writer, this);
        private static object ReadElementValue(XmlReader reader, Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsEnum)
                return Enum.Parse(type, reader.ReadElementContentAsString());
            if (type == typeof(Guid))
                return Guid.Parse(reader.ReadElementContentAsString());
            if (type.IsClass && type != typeof(string))
            {
                var serializer = new XmlSerializer(type, new XmlRootAttribute(reader.Name));
                try
                {
                    return serializer.Deserialize(reader);
                }
                catch (InvalidOperationException e)
                {
                    if (e.InnerException == null)
                        throw;
                    throw e.InnerException;
                }
            }

            return reader.ReadElementContentAs(type, null);
        }
        private static object ReadContentValue(XmlReader reader, Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsEnum)
                return Enum.Parse(type, reader.ReadContentAsString());
            if (type == typeof(Guid))
                return Guid.Parse(reader.ReadContentAsString());

            return reader.ReadContentAs(type, null);
        }
        private static object ReadElementValue(XmlReader reader, List<Type> types)
        {
            Type matchedType = types.FirstOrDefault(x => x.IsPrimitive || x == typeof(string) ? String.Equals(x.Name, reader.Name, StringComparison.OrdinalIgnoreCase) : x.Name == reader.Name);
            if (matchedType == null)
                throw new XmlException(types.Count > 1 ? $"Element '{reader.Name}' does not match any of the following:\r\n" + string.Join("\r\n", types.Select(x => x.Name)) :
                    $"Element '{reader.Name}' does not match expected type '{types.FirstOrDefault()?.Name}'");
            return ReadElementValue(reader, matchedType);
        }
        private bool MoveToNextElement(XmlReader reader, string enclosingElement)
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == enclosingElement)
            {
                reader.ReadEndElement();
                return false;
            }
            reader.Read();
            while (reader.NodeType != XmlNodeType.Element)
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == enclosingElement)
                {
                    reader.ReadEndElement();
                    return false;
                }
                if (!reader.Read())
                    throw new XmlException("Unexpected end of XML document.");

                if (reader.NodeType == XmlNodeType.Text)
                    throw new XmlException("Unexpected text.");
            }
            return true;
        }
    }
        
    public class OOBESoftware
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Local { get; set; } = true;
        [CanBeNull] public string IconPath { get; set; }
        public bool? IsDefaultWebBrowser { get; set; }
    }
    [Serializable]
    public class OOBE
    {
        public enum InternetRequirementLevel
        {
            Request,
            Force
        }
        [CanBeNull] public string Username { get; set; }
        [CanBeNull] public string Password { get; set; }
        [CanBeNull] public string AdminPassword { get; set; }
        [CanBeNull] public InternetRequirementLevel? InternetRequirement { get; set; } = null;
        public bool AdminUserEnabled { get; set; } = false;
        public bool AutoLogon { get; set; } = false;
        public string[] Options { get; set; }
        public bool Verified { get; set; }
        public List<BulletPoint> BulletPoints { get; set; } = new List<BulletPoint>();
        public List<OOBESoftware> Software { get; set; } = new List<OOBESoftware>();
    }
}
