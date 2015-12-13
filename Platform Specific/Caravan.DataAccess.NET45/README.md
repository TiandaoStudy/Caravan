
# Configurazione per Oracle

## Dentro il file Web.config

### Dentro il nodo "configSections"


   <!-- Oracle Configuration Section (START) -->
      <section name="oracle.manageddataaccess.client" type="OracleInternal.Common.ODPMSectionHandler, Oracle.ManagedDataAccess" />
   <!-- Oracle Configuration Section (START) -->

### Nel resto del file

   <!-- Oracle Configuration Sections (START) -->
      <connectionStrings>
         <add name="NewAGE" connectionString="Data Source=RINA11;User Id=newage;password=newage;" providerName="Oracle.ManagedDataAccess.Client" />
      </connectionStrings>

      <system.data>
         <DbProviderFactories>
            <add name="Oracle Data Provider for .NET" invariant="Oracle.ManagedDataAccess.Client" description=".Net Framework Data Provider for Oracle" type="Oracle.ManagedDataAccess.Client.OracleClientFactory, Oracle.ManagedDataAccess" />
         </DbProviderFactories>
      </system.data>

      <oracle.manageddataaccess.client>
         <version number="*">
            <settings>
               <setting name="TNS_ADMIN" value="C:\oracle\11g_32\network\admin" />
            </settings>
         </version>
      </oracle.manageddataaccess.client>

      <entityFramework>
         <defaultConnectionFactory type="Oracle.ManagedDataAccess.Client.OracleClientFactory, Oracle.ManagedDataAccess" />
         <providers>
            <provider invariantName="Oracle.ManagedDataAccess.Client"
                      type="Oracle.ManagedDataAccess.EntityFramework.EFOracleProviderServices, Oracle.ManagedDataAccess.EntityFramework" />
         </providers>
      </entityFramework>
   <!-- Oracle Configuration Sections (END) -->