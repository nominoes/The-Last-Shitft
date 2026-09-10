# THE LAST SHIFT — PROJECT CONTEXT

## Game
Title: The Last Shift

Genre:
First-person psychological horror / exploration / story-driven.

Setting:
An abandoned hospital at night.

Target:
A small but polished standalone horror game, effectively Chapter 1 of a larger story.

Core feeling:
Isolation, uncertainty, being watched, and psychological unease.

Core loop:
Explore → Investigate → Discover → Experience → Progress → Escape

## Creative Direction

The horror should be primarily psychological and atmospheric.

Avoid:
- constant jumpscares
- combat
- unnecessary enemy AI
- complicated inventory systems
- excessive mechanics

The hospital itself should feel like the antagonist.

The player should frequently question whether what they saw actually happened.

Important horror principles:
- show less rather than more
- environmental storytelling matters
- objects and rooms can subtly change
- doors can change state
- lights can change
- distant figures should often disappear when looked at again
- sound and lighting should carry much of the tension

## Story

### Act I — The Night Shift
The player arrives at the abandoned hospital at 11:47 PM.

A note says:

NIGHT SHIFT — 12:00 AM
Check Ward B before leaving.

Objective:
Check Ward B.

### Act II — The Blackout
Ward B is inaccessible because of a power failure.

The player finds the electrical room and restores power.

After power returns, a figure appears at the end of a hallway and disappears when the player looks away.

### Act III — CCTV
The player discovers the security/CCTV room.

Three cameras:
- Reception
- East/Main Corridor
- Ward B

A mysterious figure appears on the Ward B camera and seems to move closer when cameras are switched.

Eventually the player realizes the CCTV view corresponds to a corridor they are physically standing near.

### Act IV — What Happened?
The player enters Ward B.

Important discovery:
Patient 214.

Patient records describe repeated reports of a woman appearing in corridors.

Final report:
Patient missing.
Room locked from inside.

An old staff photograph contains a person whose identity has been obscured/scratched out.

### Act V — The Hospital Remembers
The player experiences increasingly strange events.

A woman appears at the end of a corridor.

She does not attack.

After the player looks away, she disappears.

Previously locked doors may now be open.

Objective:
Find the emergency exit key.

### Act VI — The Truth
The player reaches another hospital area/nurses' station.

They find the emergency exit key and a final incident report.

The hospital experienced multiple disappearances involving night-shift workers.

The last disappearance happened exactly one year ago.

A clock shows 11:47 PM.

The implication:
The player may be experiencing the final night of someone who disappeared and may be becoming part of the hospital's cycle.

### Final Act — The Exit
The player returns to the entrance and attempts to escape.

The hospital becomes increasingly unstable:
- doors slam
- lights flicker
- CCTV activates
- footsteps follow
- intercom activates
- the woman appears

The emergency door opens.

White light.

SHIFT COMPLETE

Pause.

12:00 AM

Intercom:
"Night shift begins at 11:47 PM."

Black screen.

THE LAST SHIFT

Implication:
The player may not have escaped.

## Current Level

Main scene:
Assets/Scenes/Hospital_Chapter1.unity

Current environment:
A compact primitive hospital blockout.

Major areas:
- Reception
- Main Hallway
- Ward B
- Patient Room 201
- Patient Room 202
- Patient Room 214
- Electrical / Storage Room
- Security / CCTV Room
- Emergency Exit

Current hierarchy:

Hospital
├── Architecture
├── Rooms
├── Doors
├── Lighting
└── Props

Current project folders:
Assets/
├── Scenes/
├── Scripts/
├── Materials/
├── Prefabs/
├── Models/
├── Textures/
├── Audio/
├── UI/
└── Resources/

Current state:
- PHASE 1 — Hospital environment foundation: COMPLETE
- PHASE 2 — First-person player foundation: COMPLETE
  PlayerController exists and is working (WASD movement, mouse look, gravity, collision via CharacterController).
- PHASE 2.5A — Hospital visual polish and cleanup: COMPLETE + COMMITTED
  Scene dressing/texture pass (props, materials, procedural textures) applied and committed via Git checkpoint.
- PHASE 3A — Controlled PBR pack integration: COMPLETE + COMMITTED
  PBR Hospital Horror Pack (Assets/Dnk_Dev/HospitalHorrorPack/) selected assets integrated into Hospital_Chapter1:
  - 3 Ward B patient beds rebuilt with P_Bed_01 frame + P_BedBedding (packed prefab parts), original footprints preserved, BoxColliders added.
  - 5 doors replaced (PatientRoom201, PatientRoom202, PatientRoom214, Security, Electrical) with P_Door_01_ leaf + P_Door_01_Base frame; swing preserved via existing Hinge; old jamb/leaf parts disabled.
  - 3 ceiling lamps replaced with P_Lamp (ReceptionLight_A, HallLight_A, WardLight).
  - 3 medical cabinets added with P_Med_box_01 (Ward B, Security Room, Patient Room 214).
  - Pack material Mat_Door_01_G confirmed in use on door glass; Mat_Tile01/02 NOT applied (floors already themed; atmosphere unchanged).
  Scene saved. Play Mode smoke test passed: 0 console errors, 0 warnings.
- PHASE 3B — Core Interaction System: COMPLETE + VERIFIED
  Reusable interaction framework (layer-agnostic, no game-progression knowledge):
  - IInteractable interface: CanInteract / InteractionPrompt / Interact.
  - InteractableBase abstract MonoBehaviour for future interactables to inherit from.
  - InteractionSystem on the Player (camera-center raycast, configurable distance/layer mask/key, focus handling, prompt wiring).
  - InteractionPrompt lightweight procedural legacy-UI prompt ([E] ...), shown near lower-center of screen.
  - TestInteractable (scene object "InteractionTest" near Reception) proves the pipeline.
  Scripts: Assets/Scripts/Interaction/ (5 scripts, all namespace LastShift).
  Material: Assets/Materials/Mat_InteractionTest.mat.
  All headless + Play Mode tests passed (8/8). 0 console errors.
  No gameplay progression systems (doors, fuse, key, CCTV, notes, ghost events) have been implemented yet.
  Future gameplay systems will build on the interaction framework, not replace it.

The project is no longer in a "Phase 2 / no scripts" state.

Hospital_Chapter1 is the active game scene.
The existing hospital layout and player setup must be preserved in all future work.

## Development Method

Build incrementally in phases.

Never attempt to build the entire game in one task.

For every phase:
1. Inspect current project state.
2. Plan the smallest reliable implementation.
3. Implement only that phase.
4. Verify it works.
5. Report what changed.
6. Stop.

Do not silently continue into later phases.

Do not destroy or rebuild existing working systems unless explicitly requested.

Prefer simple, reliable Unity/C# systems.

Use meaningful GameObject, script, variable, and folder names.

## Current Phase

PHASE 3B — CORE INTERACTION SYSTEM — COMPLETE

Phase 3B is done. The reusable interaction foundation is in place (IInteractable,
InteractableBase, InteractionSystem, InteractionPrompt, TestInteractable).
No gameplay progression systems (doors, fuse, key, CCTV, notes, ghost events)
have been implemented yet. Do not move into later phases; wait for explicit
direction.

## Git

The project uses Git.

main contains the Phase 1 checkpoint:
feat: create hospital environment foundation

Current development branch:
dev/player-system

Phase 2.5A + Phase 3A were saved as a single pushed checkpoint:
feat: complete hospital visual polish and asset integration (92db9ca)

Do not modify main directly.

Commit completed working milestones separately.
