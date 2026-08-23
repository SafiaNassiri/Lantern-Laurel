# Lantern & Laurel

> An atmospheric 3D third-person mystery and adventure game built in Unity.

An awkward teen takes the graveyard shift at historic Blackhollow Cemetery and discovers the dead are restless[cite: 1]. Armed with mundane caretaker tools and half-remembered folk witchcraft, they spend each night between 11 PM and 6 AM balancing daily chores with a decades-old supernatural mystery that only the dead can finish telling[cite: 1].

---

## Core Pillars

* **Routine as Ritual:** Mundane maintenance and supernatural tasks share the exact same tools and environment; ritual mechanics grow directly out of nightly chores[cite: 1].
* **Diegetic-First Design:** In-world cues replace traditional HUD clutter[cite: 1]. Lantern flicker, pendulum vibrations, and grimoire pages guide player direction and discovery[cite: 1].
* **Melancholic Mystery Box:** Cozy in its grounding routine (sweeping paths, locking gates, watering flowers) and eerie in its quiet supernatural escalation[cite: 1].
* **Modular Shift Structure:** Built around a reusable nightly cycle (Briefing $\rightarrow$ Free-Roam $\rightarrow$ Escalation $\rightarrow$ Dawn Resolution) with soft time pressure[cite: 1].

---

## Dual-Use Caretaker Toolkit

Every tool in the caretaker's kit carries both a mundane chore function and a supernatural purpose[cite: 1]:

| Tool | Mundane Chore Function | Supernatural Ritual Function |
| :--- | :--- | :--- |
| **Lantern** | Illuminates dark grounds and paths[cite: 1] | Reveals spectral footprints and cold spots via blessed-oil fuel[cite: 1] |
| **Grimoire** | Nightly chore checklist and schedule log[cite: 1] | Compendium of discovered clues, ritual recipes, and spirit fragments[cite: 1] |
| **Broom** | Cleans leaves, gravel, and pathway debris[cite: 1] | Disperses cursed ash and breaks unneeded salt warding circles[cite: 1] |
| **Shovel** | Fills sunken graves and clears yard hazards[cite: 1] | Unearths buried ward charms, bone fragments, and hidden relics[cite: 1] |
| **Watering Can** | Tends to wilted funeral flowers and greenery[cite: 1] | Fills ritual offering bowls and scrying vessels[cite: 1] |

---

## Folk Witchcraft & Ritual Verbs

The game features four core puzzle verbs that recombine across chapters[cite: 1]:

* **Chalk Circles:** Trace specific shapes near graves or anomalous sites according to grimoire formulas[cite: 1].
* **Salt Lines:** Lay salt boundaries to ward, contain, or redirect restless spirits along patrol paths[cite: 1].
* **Pendulum Dowsing:** Hold out a weighted pendulum; vibration and audio pitch guide Wren toward hidden objectives[cite: 1].
* **Herbal Brewing:** Combine harvested graveyard flora at a fixed shed station to create ritual aids[cite: 1].

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
│   ├── Audio/         # Diegetic SFX, atmospheric drones, and spirit stingers[cite: 1]
│   ├── Materials/     # Low-poly flat-shaded shaders, teal emissives, ambient palettes[cite: 1]
│   ├── Models/        # Environment modular kits, tools, and shared humanoid rigs[cite: 1]
│   ├── Prefabs/       # Interactables, spirits, tools, and ritual triggers
│   ├── Scenes/        # Blackhollow Cemetery hub and test gyms[cite: 1]
│   └── Scripts/
│       ├── Core/      # Nightly shift clock manager (11 PM - 6 AM), state machines[cite: 1]
│       ├── Chores/    # Task tracking, completion triggers, and progression gates[cite: 1]
│       ├── Spirits/   # Spirit AI, dialogue states (Released/Bound/Ignored)[cite: 1]
│       ├── Tools/     # Radial equipment wheel and dual-use tool logic[cite: 1]
│       └── UI/        # Diegetic grimoire interface, notepad chore checklist[cite: 1]
└── Packages/
```

---

## License
Private project — All rights reserved. 
Story, game design, characters, and assets are original works by Safia Nassiri.
