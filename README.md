# Lantern & Laurel

> An atmospheric 3D third-person mystery and adventure game built in Unity.

An awkward teen takes the graveyard shift at historic Blackhollow Cemetery and discovers the dead are restless. Armed with mundane caretaker tools and half-remembered folk witchcraft, they spend each night between 11 PM and 6 AM balancing daily chores with a decades-old supernatural mystery that only the dead can finish telling.

---

## Core Pillars

* **Routine as Ritual:** Mundane maintenance and supernatural tasks share the exact same tools and environment; ritual mechanics grow directly out of nightly chores.
* **Diegetic-First Design:** In-world cues replace traditional HUD clutter. Lantern flicker, pendulum vibrations, and grimoire pages guide player direction and discovery.
* **Melancholic Mystery Box:** Cozy in its grounding routine (sweeping paths, locking gates, watering flowers) and eerie in its quiet supernatural escalation.
* **Modular Shift Structure:** Built around a reusable nightly cycle (Briefing $\rightarrow$ Free-Roam $\rightarrow$ Escalation $\rightarrow$ Dawn Resolution) with soft time pressure.

---

## Dual-Use Caretaker Toolkit

Every tool in the caretaker's kit carries both a mundane chore function and a supernatural purpose:

| Tool | Mundane Chore Function | Supernatural Ritual Function |
| :--- | :--- | :--- |
| **Lantern** | Illuminates dark grounds and paths | Reveals spectral footprints and cold spots via blessed-oil fuel |
| **Grimoire** | Nightly chore checklist and schedule log | Compendium of discovered clues, ritual recipes, and spirit fragments |
| **Broom** | Cleans leaves, gravel, and pathway debris | Disperses cursed ash and breaks unneeded salt warding circles |
| **Shovel** | Fills sunken graves and clears yard hazards | Unearths buried ward charms, bone fragments, and hidden relics |
| **Watering Can** | Tends to wilted funeral flowers and greenery | Fills ritual offering bowls and scrying vessels |

---

## Folk Witchcraft & Ritual Verbs

The game features four core puzzle verbs that recombine across chapters:

* **Chalk Circles:** Trace specific shapes near graves or anomalous sites according to grimoire formulas.
* **Salt Lines:** Lay salt boundaries to ward, contain, or redirect restless spirits along patrol paths.
* **Pendulum Dowsing:** Hold out a weighted pendulum; vibration and audio pitch guide Wren toward hidden objectives.
* **Herbal Brewing:** Combine harvested graveyard flora at a fixed shed station to create ritual aids.

---

## Built With

* **Engine:** Unity (6000.4.11f1 / Unity 6)
* **Language:** C#
* **Target Platforms:** PC / Windows 64-bit

---

## Getting Started

### Prerequisites
* **Unity Hub** with **Unity 6 (6000.4.11f1)** or compatible 6000.x stream installed.
* Visual Studio 2022 / JetBrains Rider with Unity workload.

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/SafiaNassiri/Lantern-Laurel.git
   ```
2. Open Unity Hub.
3. Click Add -> Add project from disk and select the cloned `Lantern-Laurel` root directory.
4. Launch the project using Unity version `6000.4.11f1`.
5. Open `Assets/Scenes/Main.unity` (or designated bootstrap scene) and press Play.

---

## Project Structure
```
Lantern-Laurel/
├── Assets/
│   ├── Audio/         # Diegetic SFX, atmospheric drones, and spirit stingers
│   ├── Materials/     # Low-poly flat-shaded shaders, teal emissives, ambient palettes
│   ├── Models/        # Environment modular kits, tools, and shared humanoid rigs
│   ├── Prefabs/       # Interactables, spirits, tools, and ritual triggers
│   ├── Scenes/        # Blackhollow Cemetery hub and test gyms
│   └── Scripts/
│       ├── Core/      # Nightly shift clock manager (11 PM - 6 AM), state machines
│       ├── Chores/    # Task tracking, completion triggers, and progression gates
│       ├── Spirits/   # Spirit AI, dialogue states (Released/Bound/Ignored)
│       ├── Tools/     # Radial equipment wheel and dual-use tool logic
│       └── UI/        # Diegetic grimoire interface, notepad chore checklist
└── Packages/
```

---

## License
Private project — All rights reserved. 
Story, game design, characters, and assets are original works by Safia Nassiri.
