@echo off

rem スクリプトが置かれている場所をカレントディレクトリにする
cd /d %~dp0

rem テンプレートディレクトリ定義
set key[0]=Scenes
set key[1]=Prefabs
set key[2]=Scripts
set key[3]=Animations
set key[4]=Materials
set key[5]=Physics Materials
set key[6]=Fonts
set key[7]=Textures
set key[8]=Audio
set key[9]=Resources
set key[10]=Editor
set key[11]=Plugins
set key[12]=Tiles
setlocal ENABLEDELAYEDEXPANSION

for /L %%i in (0,1,12) do (
 mkdir !key[%%i]!
)

for /L %%i in (0,1,12) do (
 type nul > !key[%%i]!\empty
)
