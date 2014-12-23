Import-Module VisioPS

$xmldoc = @"
<orgchart>
  <shape id="0" name="Akuma" />
  <shape id="1" name="Ryu" parentid="0"/>
  <shape id="2" name="Ken" parentid="0"/>
  <shape id="3" name="Chun-Li" parentid="2"/>
</orgchart>
"@

$mydocs = [System.Environment]::GetFolderPath("MyDocuments") 
$filename = join-path $mydocs "visiops-demo-orgchart.xml"
$xmldoc | out-file $filename 

$orgchart= Import-VisioOrgChart -Filename $filename 
New-VisioApplication
Invoke-VisioDraw $orgchart
