###############################################################################
#
# install.ps1 --
#
# Written by Joe Mistachkin.
# Released to the public domain, use at your own risk!
#
###############################################################################

param($installPath, $toolsPath, $package, $project)

$platformNames = "x86", "x64"
$dtc = "Oracle.ManagedDataAccessDTC.dll"
$iop = "Oracle.ManagedDataAccessIOP.dll"
$propertyName = "CopyToOutputDirectory"

foreach($platformName in $platformNames) {
  $folder = $project.ProjectItems.Item($platformName)
  
  if ($folder -eq $null) {
    continue
  }

  $item = $folder.ProjectItems.Item($dtc)
  if ($item -eq $null) {
    continue
  }
  $property = $item.Properties.Item($propertyName)
  if ($property -ne $null) {
    $property.Value = 2
  }

  $item = $folder.ProjectItems.Item($iop)
  if ($item -eq $null) {
    continue
  }
  $property = $item.Properties.Item($propertyName)
  if ($property -ne $null) {
    $property.Value = 2
  }
}
