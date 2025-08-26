# 🎮 Unity Projects Collection

This repository showcases various gameplay mechanics, systems, and Unity features through three distinct projects.

## 📋 Table of Contents
- [🛠️ Requirements](#️-requirements)
- [🎯 Projects Overview](#-projects-overview)
- [🚀 Quick Start](#-quick-start)
- [📱 Project Details](#-project-details)
- [🎨 Assets & Resources](#-assets--resources)
- [📸 Screenshots](#-screenshots)
- [🤝 Contributing](#-contributing)

## 🛠️ Requirements

- **Unity Version**: `2022.3.10f1` (LTS)
- **Platform**: Windows/Mac/Linux
- **Additional Packages**:
  - TextMesh Pro
  - Unity NavMesh Components
  - Unity Visual Scripting

## 🎯 Projects Overview

| Project | Type | Complexity | Key Features |
|---------|------|------------|--------------|
| [BasicMovement](#-basicmovement) | Character Controller | ⭐ Beginner | WASD Movement, Jumping, Camera Control |
| [ClickToMove](#-clicktomove) | Navigation System | ⭐⭐ Intermediate | Click-to-Move, NavMesh, Isometric Camera |
| [UnityTemelleri](#-unitytemelleri) | Action RPG/Tower Defense | ⭐⭐⭐⭐⭐ Advanced | Complete Game System |

## 🚀 Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/ekrem-bas/Unity-Projects.git
   ```

2. **Open in Unity Hub**
   - Launch Unity Hub
   - Click "Add" and select one of the project folders
   - Ensure Unity 2022.3.10f1 is installed

3. **Run the project**
   - Open the main scene in the `Scenes` folder
   - Press Play ▶️ to start!

## 📱 Project Details

## 🏃 BasicMovement

**A fundamental character movement system perfect for learning Unity basics.**

### 🎮 Features

#### 🎯 Player Movement System
- **WASD Controls**: Smooth character movement in all directions
- **Camera-Relative Movement**: Movement direction based on camera orientation
- **Physics-Based**: Uses Rigidbody for realistic movement
- **Jump Mechanics**: Spacebar jumping with ground detection
- **Speed Control**: Adjustable movement speed via inspector

#### 📷 Camera System
- **Third-Person Camera**: Follows player with smooth movement
- **Mouse Look**: Free-look camera control with mouse
- **Angle Constraints**: Prevents camera flipping (30° to 60° vertical range)
- **Customizable Offset**: Adjustable camera distance and height
- **Sensitivity Settings**: Configurable mouse sensitivity

#### 🔧 Technical Implementation
```csharp
// Key Code Features:
- Transform.Translate() for movement
- Camera forward/right vectors for direction
- Ground detection via collision tags
- Rigidbody.AddForce() for jumping
- Quaternion.Euler() for camera rotation
```

#### 📁 File Structure
```
BasicMovement/
├── Assets/
│   ├── Scenes/          # Game scenes
│   └── Scripts/
│       ├── PlayerMovement.cs    # Character movement logic
│       └── CameraController.cs  # Third-person camera control
```

---

## 🖱️ ClickToMove

**An elegant point-and-click navigation system using Unity's NavMesh.**

### 🎮 Features

#### 🎯 Navigation System
- **Click-to-Move**: Intuitive point-and-click movement
- **NavMesh Integration**: Intelligent pathfinding around obstacles
- **Ground Layer Detection**: Ensures valid movement areas only
- **Smooth Movement**: Natural character animation and movement

#### 📷 Isometric Camera
- **Fixed Angle View**: Perfect isometric perspective (45°, 45°, 0°)
- **Smooth Following**: Camera smoothly follows player movement
- **Configurable Offset**: Adjustable camera position relative to player
- **Performance Optimized**: Efficient LateUpdate camera movement

#### 🔧 Technical Implementation
```csharp
// Key Code Features:
- NavMeshAgent for pathfinding
- Camera.ScreenPointToRay() for mouse picking
- LayerMask for ground detection
- Vector3.Lerp() for smooth camera movement
- Physics.Raycast() for click detection
```

#### 📁 File Structure
```
ClickToMove/
├── Assets/
│   ├── New Terrain.asset    # Terrain with NavMesh
│   ├── Scenes/             # Game scenes
│   └── Scripts/
│       ├── Player.cs           # Click-to-move player control
│       └── CameraController.cs # Isometric camera system
```

---

## 🏰 UnityTemelleri

**A complete action RPG with tower defense elements - the crown jewel of this collection!**

### 🎮 Core Game Systems

#### 👤 Player System
- **Character Selection**: Multiple character prefabs to choose from
- **NavMesh Movement**: Click-to-move with intelligent pathfinding
- **Health System**: Visual health bar with damage feedback
- **Animation Controller**: Running, idle, and death animations
- **Coin Collection**: Economy system with visual coin counter

#### ⚔️ Combat System
- **Projectile Shooting**: Player can shoot bullets at enemies
- **Skill System**: Two powerful magic skills (Meteor & Beam)
- **Target Selection**: Visual cursor changes for skill targeting
- **Damage Calculation**: Balanced damage system across all weapons

#### 🏹 Advanced Skill System

##### 🌟 Meteor Skill
```csharp
// Meteor Features:
- Area of Effect (AOE) damage
- Physics-based falling meteor
- Visual impact effects
- Configurable damage and fall speed
- Smart enemy detection within blast radius
```

##### ⚡ Beam Skill
```csharp
// Beam Features:
- Single-target homing attack
- Precise enemy tracking
- High damage output
- Visual beam effects from sky
- Instant enemy targeting
```

#### 🤖 Enemy AI System

##### ⚔️ Swordsman Enemy
- **Melee Combat**: Close-range sword attacks
- **Chase Behavior**: Actively pursues the player
- **Attack Range**: Engages in combat when close enough
- **Sword Collision**: Realistic sword hitbox during attacks

##### 🧙 Wizard Enemy
- **Ranged Combat**: Fires magic projectiles at player
- **Optimal Distance**: Maintains strategic distance from player
- **Magic Attacks**: Homing magic missiles
- **Line of Sight**: Always faces player when attacking

#### 🏗️ Tower Defense System
- **Tower Placement**: Strategic tower building system
- **Tower Types**: Multiple tower configurations with different stats
- **Automated Shooting**: Towers automatically target and shoot enemies
- **Upgrade System**: ScriptableObject-based tower data
- **Resource Management**: Coin-based tower purchasing

#### ⚡ Performance Optimization

##### 🔄 Object Pooling System
```csharp
// Pooled Objects:
- Enemy Pool Manager (Efficient enemy spawning)
- Bullet Pool Manager (Player projectiles)
- Magic Pool Manager (Enemy projectiles)  
- Skill Pool Manager (Meteor & Beam effects)
- Tower Bullet Pool Manager (Tower projectiles)
```

#### 🎮 Game State Management
- **Game Manager**: Centralized state control
- **Player Death Handling**: Game over screen integration
- **Skill Selection States**: UI state management
- **Scene Management**: Smooth transitions between game states

#### 🎨 Visual & UI Systems
- **Health Bars**: Dynamic health visualization for all entities
- **Skill UI**: Interactive skill selection interface
- **Cursor Management**: Context-sensitive cursor changes
- **Particle Effects**: Visual feedback for all actions
- **TextMesh Pro**: Advanced text rendering system

### 🔧 Technical Architecture

#### 📊 Data Management
```csharp
// ScriptableObjects:
- PlayerData.cs     # Player stats and configuration
- TowerData.cs      # Tower stats and pricing
- Character Prefabs # Multiple selectable characters
```

#### 🎯 Design Patterns Used
- **Singleton Pattern**: GameManager, SkillManager
- **Object Pooling**: Performance optimization
- **Component System**: Modular, reusable components
- **Observer Pattern**: Health and UI updates
- **State Machine**: Game state management

#### 📁 File Structure
```
UnityTemelleri/
├── Assets/
│   ├── Animations/          # Character animations
│   ├── AnimatorControllers/ # Animation state machines
│   ├── Images/             # UI sprites and textures
│   ├── Materials/          # 3D materials
│   ├── Prefabs/           # Reusable game objects
│   ├── Scenes/            # Game scenes
│   ├── Scripts/
│   │   ├── Common/        # Shared utilities
│   │   ├── Enemy/         # Enemy AI and behavior
│   │   ├── Player/        # Player systems
│   │   ├── SkillManagement/ # Magic skill system
│   │   ├── ObjectPooling/   # Performance optimization
│   │   └── Tower/           # Tower defense system
│   ├── OccaSoftware/      # Third-party crosshair assets
│   ├── ParticleProFX/     # Particle effect systems
│   ├── TextMesh Pro/      # Advanced text rendering
│   └── SkySeries Freebie/ # Skybox assets
```

## 🎨 Assets & Resources

### 🔧 Third-Party Assets
- **OccaSoftware Crosshairs**: Professional crosshair system
- **ParticleProFX**: High-quality particle effects
- **SkySeries Freebie**: Beautiful skybox collection

### 🎨 Visual Effects
- Particle systems for magic skills
- Blood effects for combat feedback
- Death animations and effects

## 🔧 Technical Highlights

### 💻 Programming Concepts Demonstrated
- **Object-Oriented Programming**: Clean class hierarchies
- **Component-Based Architecture**: Modular Unity systems
- **Performance Optimization**: Object pooling patterns
- **State Management**: Game state and UI state handling
- **Physics Integration**: Rigidbody and collision systems
- **AI Programming**: Enemy behavior trees
- **UI Programming**: Dynamic interface updates

### 🎯 Unity Features Utilized
- **NavMesh System**: Intelligent pathfinding
- **Animation System**: Character animation controllers
- **Physics System**: Collision detection and Rigidbody dynamics
- **Particle System**: Visual effects and feedback
- **ScriptableObjects**: Data-driven design
- **Coroutines**: Asynchronous programming

## 🤝 Contributing

Feel free to:
- 🐛 Report bugs or issues
- 💡 Suggest new features or improvements
- 🔧 Submit pull requests with enhancements
- 📖 Improve documentation

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 👨‍💻 Author

**Ekrem Baş**
- GitHub: [@ekrem-bas](https://github.com/ekrem-bas)

---

*Happy coding and game development! 🎮✨*
