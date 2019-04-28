@ECHO OFF
pushd "../Libs/GenEntitas/GenEntitas.Runner.Console/bin/debug"
GenEntitas.exe --SettingsPath="../../../../../Core/Generator.config"
popd