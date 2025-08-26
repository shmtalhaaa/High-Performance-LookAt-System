# High-Performance-LookAt-System

The **High-Performance LookAt System** is a lightweight and scalable Unity solution for rotating GameObjects toward targets smoothly and efficiently.  
It is designed for projects ranging from small interactive scenes to large-scale games, capable of handling **500+ objects at 60 FPS** and scaling up to **2,000 objects in lighter scenes**.

---

## ✨ Features
- 🎯 **Smooth Rotation** – Uses `Quaternion.Slerp` for natural, non-snapping motion.  
- ⚡ **High Performance** – Centralized update loop removes redundant `Update()` calls.  
- 🔒 **Axis Constraints** – Lock rotation on X, Y, or Z axes.  
- 📐 **Rotation Limits** – Define per-axis min/max angles.  
- 🔁 **Continuous or One-Time Tracking** – Track targets constantly or stop once aligned.  
- 🔔 **UnityEvents Integration** – Trigger events on rotation start/stop for custom logic.  

---

## 📂 Installation
1. Download or clone this repository: https://github.com/shmtalhaaa/High-Performance-LookAt-System.git
2. Copy the Scripts folder into your Unity project.
3. Add the LookAtTarget component to any GameObject you want to rotate toward a target.
4. Add the LookAtManager component to any other GameObject.
5. Add target to LookAtTarget and enter play mode to see results and stats.

⚡ Performance
✅ Optimized with a centralized manager.
✅ Efficient enough for 500+ active objects at 60 FPS on modern hardware.
✅ Scales to 1,000–2,000 objects in less complex scenes.
✅ Ideal for AI, turrets, cinematic cameras, crowds, and swarm systems.

🔑 Example Use Cases
Enemies or NPCs tracking the player.
Turrets automatically aiming at multiple targets.
Cinematic cameras focusing on points of interest.
Large-scale simulations with crowds or swarms.

🛠️ Extending
Add custom events for special actions on rotation.
Modify the manager to support priority-based target tracking.
Integrate with AI systems for smarter behavior.
