# AudioClipAssetReplacer
Unity tool that allows you replace AudioClip without compile it into Unity Editor.

Used libraries:
- [AssetsTools.NET](https://github.com/nesrak1/AssetsTools.NET/tree/main) for assets file editing.
- [AssetRipper.FSBank.V1](https://www.nuget.org/packages/AssetRipper.FSBank.V1) for converting audio files to the [FSB format](https://www.fmod.com).
- [AssetRipper.FMOD.V1](https://www.nuget.org/packages/AssetRipper.FMOD.V1) for converting audio files to the [FSB format](https://www.fmod.com).
- [Fmod5Sharp](https://github.com/SamboyCoding/Fmod5Sharp) for converting the FSB files to an audio files.
- [NAudio](https://github.com/naudio/NAudio) for audio playing.
- [NAudio.Vorbis](https://github.com/naudio/Vorbis) for OGG(Vorbis) audio support.

This project contains wrapping code and the native libraries for FMOD 1.10.20 (FMOD Studio, copyright © Firelight Technologies Pty, Ltd., 1994-2016.).
This project is not affiliated with Unity Technologies or Firelight Technologies.
