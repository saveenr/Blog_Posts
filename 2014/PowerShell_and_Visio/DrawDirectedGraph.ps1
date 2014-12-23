Import-Module VisioPS

$xmldoc = @"
<directedgraph>
  <page>
    <renderoptions
      usedynamicconnectors="true"
      scalingfactor="20"
    />
    <shapes>
      <shape id="n1" label="FOO1" stencil="server_u.vss" master="Server" url="http://microsoft.com" />
      <shape id="n2" label="FOO2" stencil="server_u.vss" master="Email Server" url="http://contoso.com"/>
      <shape id="n3" label="FOO3" stencil="server_u.vss" master="Proxy Server" url="\\isotope\public" />
      <shape id="n4" label="FOO4" stencil="server_u.vss" master="Web Server">
        <customprop name="prop1" value="value1"/>
        <customprop name="prop2" value="value2"/>
        <customprop name="prop3" value="value3"/>
      </shape>
      <shape id="n5" label="FOO4" stencil="server_u.vss" master="Application Server" />
    </shapes>
    <connectors>
      <connector id="c1"  from="n1" to="n2" label="LABEL1" />
      <connector id="c2" from="n2" to="n3" label="LABEL2" color="#ff0000" weight="2" />
      <connector id="c3" from="n3" to="n4" label="LABEL1" color="#44ff00" />
      <connector id="c4" from="n4" to="n5" label="" color="#0000ff" weight="5"/>
      <connector id="c5" from="n4" to="n1" label="" />
      <connector id="c6" from="n4" to="n3" label="" weight="10"/>
    </connectors>
  </page>
</directedgraph>
"@

$mydocs = [System.Environment]::GetFolderPath("MyDocuments") 
$filename = join-path $mydocs "visiops-demo-dg.xml"
$xmldoc | out-file $filename 




$dg = Import-VisioDirectedGraph $filename

New-VisioApplication
Invoke-VisioDraw $dg
