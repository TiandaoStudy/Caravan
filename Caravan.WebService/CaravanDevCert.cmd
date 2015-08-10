makecert.exe ^
-n "CN=DevRoot" ^
-r ^
-pe ^
-a sha512 ^
-len 4096 ^
-cy authority ^
-sv CaravanDevCert.pvk ^
CaravanDevCAROOT.cer

pvk2pfx.exe ^
-pvk CaravanDevCert.pvk ^
-spc CaravanDevCAROOT.cer ^
-pfx CaravanDevCert.pfx ^
-po FinsaPassword