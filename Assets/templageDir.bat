@echo off

rem �X�N���v�g���u����Ă���ꏊ���J�����g�f�B���N�g���ɂ���
cd /d %~dp0

rem �e���v���[�g�f�B���N�g����`
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
