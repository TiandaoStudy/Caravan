// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following set of attributes.
// Change these attribute values to modify the information associated with an assembly.
[assembly: AssemblyTitle("Finsa.Caravan.Common")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Finsa S.p.A.")]
[assembly: AssemblyProduct("Finsa.Caravan.Common")]
[assembly: AssemblyCopyright("Copyright © Finsa S.p.A. 2015-2025")]
[assembly: AssemblyTrademark("Finsa S.p.A.")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible to COM components. If
// you need to access a type in this assembly from COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
// 
// Major Version Minor Version Build Number Revision
// 
// You can specify all the values or you can default the Build and Revision Numbers by using the '*'
// as shown below: [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.45.0")]
[assembly: AssemblyFileVersion("1.45.0")]

// Attributes added to allow stronger development.
[assembly: InternalsVisibleTo("Finsa.Caravan.DataAccess")]
[assembly: InternalsVisibleTo("Finsa.Caravan.WebApi")]
[assembly: InternalsVisibleTo("Finsa.Caravan.WebForms")]
[assembly: InternalsVisibleTo("UnitTests.Common")]