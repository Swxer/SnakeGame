# Snake-CLI-Multiplayer

A real-time, networked game engine built with **C#** and **.NET 8**. This project focuses on high-precision terminal rendering, asynchronous engine architecture, and upcoming scalable multiplayer synchronization using **SignalR**.

## System Architecture

The project is currently transitioning from a local console application to a decoupled **Server-Client** model where game state is managed centrally.

### 1. The Asynchronous Engine (Core)
The engine utilizes a non-blocking heartbeat to manage game logic without freezing the execution thread:
* **`PeriodicTimer`:** Implements a precise 100ms tick rate (10 FPS). Unlike `Thread.Sleep`, this allows the thread to be released to the thread pool during idle periods, significantly reducing CPU overhead.
* **Task-Based Asynchronous Pattern (TAP):** The entry point is an `async Task`, enabling the engine to eventually handle simultaneous network I/O and game logic.

### 2. Optimized CLI Rendering
To achieve smooth graphics in a command-line interface:
* **Manual Cursor Management:** Instead of clearing the entire console (which causes heavy flickering), the engine uses `Console.SetCursorPosition(0, 0)` to reset the draw point.
* **Frame Overwriting:** By overwriting the previous frame's characters directly, the engine minimizes terminal draw calls and provides a stable visual experience.


## Project Status: Work In Progress
The project is currently moving into **Enterprise Networking** integration:
* **SignalR Hubs:** Implementing real-time bi-directional communication between the server and remote CLI clients.
* **Thread Safety:** Migrating the game state to `ConcurrentDictionary<string, Snake>` to resolve potential race conditions in a multi-user environment.
* **State Broadcasting:** Developing the JSON snapshot mechanism to push global game updates to all connected players at the end of every engine tick.

## Tech Stack
* **Runtime:** .NET 8
* **Language:** C# 12
* **Networking:** ASP.NET Core SignalR (In Development)
* **Version Control:** Git
