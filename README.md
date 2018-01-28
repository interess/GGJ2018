# GGJ2018
Global Game Jam 2018, "Code Sword" team

# Architecture
## FFramework
FFramework - is a minimalistic framework that helps to control execution flow, ensure right order of script initalization and provide simple alternative to Dependency Injection.

### FUnit
`FUnit` is a basic logic unit. It has a list of callbacks that serve as alternative to native Unity3D callbacks.

All `MonoBehaviour` scripts in the game must inherit from `FUnit`.

### FContext
`FContext` is a partial class that is injected to all `FUnit` scripts.

It can store any references.

Instead of using Singletons - it's better to have a field in `FContext` that will hold reference to that potential Singleton class.

It can contain shortcut methods for searching other objects, or provide reference for services that do that.

From any part of appliaction feel free to add stuff to create additional partial classes `FContext` with fields and methods, that should accessible from any part of appliaction.

### FKernel
`FKernel` is a central script of application. It initializes and executes all `FUnit` scripts and injects `FConext`.

### IFTickable
Implement different `ITickable` interfaces as an alternative to `Update`, `LateUpdate`, `FixedUpdate`.

### Fuid
It is a unique id of FFramework. Every `FUnit` has it's own unique id.

It can be used to identify scripts and entities in game. 

Each `FUnit` gets uniqe id automatically and it can not be changed.
