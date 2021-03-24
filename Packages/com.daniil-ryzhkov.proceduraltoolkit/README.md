# ProceduralToolkit
This is a toolkit for procedural content generation for Unity.

## Get started
* Navigate to *GameObject* -> *ProceduralToolkit* -> *New LandscapeGenerator*
* Locate your newly created *LandscapeGenerator* object on your scene and edit settings.

## LandscapeGenerator settings
### Diamond Square
- *Seed*: sets the seed for the randomizer. This ensures the equality of generation.
- *Magnitude*: the maximum amount of magnitude relative to terrain Y size.
- *Hardness*: the hardness of smoothing of the shape. Higher value means smoother shape.
- *Bias*: the relative center elevation level. Setting this value too low or too high will plane out the bottom or the top of the shape respectively.

## Tips
- The generation resolution is adjusted automatically based on terrain heightmap resolution. So if you want to adjust that, change the overall resolution of the terrain heightmap.
- The maximum altitude of the terrain can be adjusted by combining two settings: the Y size of the terrain itself, or the *magnitude* parameter in *Diamond Square* component.

## Changelog
### v1.0.0
- Landscape generated using Diamond Square algorithm.
- Generation result is stored into a terrain heightmap.
- Implemented simple DI container.
### v0.2.1
- Implemented simple rectangle creation within a static mesh.