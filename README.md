# Udvar-Hazy-Center_Map

# 🗺️ Udvar-Hazy Museum Navigation App (Unity)

A mobile-friendly 3D navigation experience built in Unity for the **Smithsonian Udvar-Hazy Center**. Designed to help visitors explore the museum more easily by combining an interactive map, real-time user tracking, and visual waypoint guidance.

---

## Features

- **2D map** of the museum with labeled zones
- **Waypoints** for key locations (e.g., ATMs, Restrooms, Exhibits)
- **Category buttons** to highlight destinations
- **Automatic camera zoom** to frame user and closest destination
- **Dynamic lines** connecting the user to selected waypoints
- **Top-down controls** with responsive movement
- **Attached camera** follows the user as they navigate

---

## Coming Soon

- **QR Code-based indoor positioning**  
  Scan a code at any exhibit to instantly update your location on the map.
- **Custom icons and labels** for each waypoint
- Export as Android/iOS build for museum deployment
- **Breadcrumb trail** of visited locations
- Optional audio guidance or AR overlay support

---

## Tech Stack

- **Unity 6**
- C# Scripts
- Unity UI Toolkit / uGUI
- Optimized for Mobile Devices

---

## How to Use

1. Move your character using arrow keys or on-screen controls
2. Tap a button (ATM, Food, Restroom, etc.) to highlight all related waypoints
3. Watch the map auto-zoom to fit your position and the nearest destination
4. Follow the dynamic line path to explore

---

## Architecture Overview

- `WaypointManager.cs` — Manages waypoints, user input, camera framing, and line logic
- `UserMovement.cs` — Handles simple directional movement for the user avatar
- `LinePrefab` — Uses `LineRenderer` to visualize paths
- `Waypoint.cs` — Stores metadata for each point of interest

---

## Folder Structure

```
Assets/
├── Prefabs/
│ └── Waypoint.prefab
├── Scripts/
│ ├── WaypointManager.cs
│ ├── UserMovement.cs
├── UI/
│ └── Canvas + Buttons
├── Images/
│ └── ReadmeThumbnail.png
└── Textures/
└── Map overlays, lines, icons
```

---

## Contributions

Want to help improve this experience? Add custom pathfinding, support localization, or build a multi-floor navigation system — PRs welcome!

---

## Made For

[**Smithsonian National Air and Space Museum – Udvar-Hazy Center**](https://airandspace.si.edu/visit/udvar-hazy-center)  
By [Jake](https://github.com/jakep84), 2025
