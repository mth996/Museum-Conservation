# Museum Conservation VR

A Unity-based Virtual Reality application developed for **Muzium Negara (National Museum of Malaysia)** in collaboration with **Asia Pacific University of Technology & Innovation (APU)**.

The project explores how immersive technology can support **museum artifact inspection and conservation workflows**, allowing users to examine artifacts in VR, record visible forms of damage, assess severity, and visualize the resulting condition data.

## Project Overview

Museum conservation requires careful inspection and documentation of artifact condition. This project translates parts of that workflow into an immersive VR environment designed for Meta Quest.

The application provides interactive systems for examining digitized museum artifacts and documenting different forms of deterioration across multiple areas of an object.

A central component of the project is a **custom artifact damage assessment and visualization system** developed in C#.

## My Role

**Unity / VR Developer**

My responsibilities included:

- Developing the application in Unity using C#
- Implementing artifact inspection and damage-assessment systems
- Building interactive UI workflows for conservation data entry
- Developing damage severity calculations
- Creating runtime visualization of assessment results
- Implementing artifact orientation and inspection interactions
- Integrating VR interaction and navigation
- Building supporting environmental interaction systems

## Core Damage Assessment System

The application allows damage observations to be recorded across multiple categories and areas of an artifact.

The system:

1. Collects damage severity values from the inspection interface.
2. Groups observations into damage categories.
3. Calculates accumulated severity for each category.
4. Calculates an overall artifact damage percentage.
5. Updates assessment results dynamically.
6. Generates a visual representation of the resulting damage distribution.

**Artifact Inspection → Damage Input → Severity Processing → Condition Calculation → Visualization**

## Multi-Surface Damage Identification

Artifacts can contain different forms of damage on different sides. The application supports damage observations across:

- Front
- Back
- Left
- Right

Damage types represented in the current implementation include:

- Fracture
- Corrosion
- Decay
- Scratches

The system consolidates observations from different surfaces and identifies the unique damage categories currently present on the artifact.

## Runtime Data Visualization

Assessment results are converted into visual feedback inside Unity. The application dynamically generates chart segments representing the calculated contribution of different damage categories, providing an immediate visual summary of the artifact's recorded condition.

## Interactive Artifact Inspection

The interface includes an artifact orientation system that allows different inspection views to be selected. Selecting an inspection option opens the corresponding interface, rotates the artifact toward the required viewing position using coroutine-based smooth rotation, and allows the user to return the artifact to its original orientation.

## Additional Interaction Systems

Supporting Unity systems include:

- Trigger-based environmental interactions
- Coroutine-driven object animation
- Interactive doors with audio feedback
- Video playback controls
- Canvas and UI state management
- TextMeshPro-based information displays
- Event-driven Unity UI controls

## Technologies

- Unity
- C#
- Meta Quest / Virtual Reality
- TextMeshPro
- Unity UI
- Unity VideoPlayer
- Coroutines
- LINQ
- Collections / Dictionaries
- 3D scanned artifact models

## Repository Structure

```text
Museum-Conservation/
├── github/
│   ├── Scripts/
│   │   ├── ArtifactDamageCalculator.cs
│   │   ├── MultipleDamageDisplayController.cs
│   │   ├── ButtonCanvasController.cs
│   │   ├── TriggerDoorController.cs
│   │   └── VideoController.cs
│   ├── manifest.json
│   └── ProjectVersion.txt
└── README.md
```

## Technical Focus

This repository primarily demonstrates my work in:

**Unity C# Development • VR Interaction • Runtime Data Processing • Interactive UI • 3D Object Interaction • Data Visualization • Coroutine-Based Systems**

## Screenshots / Demo

Project screenshots, VR interaction examples, and damage-assessment interface demonstrations will be added here.

## Project Context

Developed as part of a collaboration between **Muzium Negara (National Museum of Malaysia)** and **Asia Pacific University of Technology & Innovation (APU)**.

**Development period:** November 2024 – February 2025  
**Role:** Game Developer / Unity VR Developer

## Note

This repository is intended as a technical portfolio showcase. Selected source code and project documentation are presented to demonstrate the systems developed without distributing third-party or restricted project assets.
