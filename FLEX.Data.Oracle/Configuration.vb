Imports System.Configuration

Public NotInheritable Class Configuration
   Inherits ConfigurationSection

   Private Const SectionName As String = "FlexDataConfiguration"
   Private Const OracleRunnerKey As String = "OracleRunner"
   Private Const OracleStatementCacheSizeKey As String = "OracleStatementCacheSize"

   Private Shared ReadOnly CachedInstance As Configuration = TryCast(ConfigurationManager.GetSection(SectionName), Configuration)

   Public Shared ReadOnly Property Instance() As Configuration
      Get
         Return CachedInstance
      End Get
   End Property

   <ConfigurationProperty(OracleRunnerKey, IsRequired:=False, DefaultValue:="")>
   Public ReadOnly Property OracleRunner() As String
      Get
         Dim runner = TryCast(Me(OracleRunnerKey), String)
         Return If(String.IsNullOrWhiteSpace(runner), runner, runner & ".")
      End Get
   End Property

   <ConfigurationProperty(OracleStatementCacheSizeKey, IsRequired:=False, DefaultValue:=10)>
   Public ReadOnly Property OracleStatementCacheSize() As Integer
      Get
         Return CType(Me(OracleStatementCacheSizeKey), Integer)
      End Get
   End Property
End Class
