# How to Deliver Tasks

Welcome to the repository.

Please read through the whole document before you begin.

## Setup

0. Make sure you have Python 3.10 installed in your global environment.
   - Check by running `python --version` in your terminal.
   - If you do not have Python 3.10, download it from
     [python.org](https://www.python.org/downloads/release/python-3100/).
1. Make sure you have .NET 9.0 or above installed in your global environment.
   - Check by running `dotnet --version` in your terminal.
   - If you have an earlier version than .NET 9.0, update it from [Microsoft: Update development environment](https://learn.microsoft.com/en-us/dotnet/core/install/upgrade#upgrade-development-environment)
   - If you do not have .NET 9.0, install it from [Download .NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
2. Clone this repository to your computer and open it in Visual Studio Code.
3. Make sure IntelliSense works. It should if you have the following
   VS Code extensions installed:
   - **C#** (by Microsoft)
   - **C# Dev Kit** (by Microsoft)
4. Check out your dedicated trunk branch. It is called `<your_email>-trunk` (e.g. `babb-trunk`). This is your personal workspace — you will only create pull requests into this branch, never into `main`.

## Finding the Tasks

From the repository's root page on GitHub, click **Projects** in the top menu bar. Open the project called **CoDepend**. You should see 6 tasks listed. These are the tasks you need to implement. Please complete them in order from the smallest issue number to the largest.

## Working on a Task

Repeat the following cycle for each task:

1. Make sure you are on your own `TRUNK` branch (it will be named with your email prefixed to `trunk`, e.g. `babb-trunk`).
2. Create a new branch from `TRUNK`, named with your email prefix and the issue number (e.g. `babb-1`).
3. Implement the task and all related subtasks. A task is complete when you have fulfilled every acceptance criterion listed in the issue, the code compiles, and all tests pass.
4. Create a Pull Request to merge your task branch into your `TRUNK` branch. **Do not merge the PR and do not request a review** — simply leave it open.
5. Switch back to your `TRUNK` branch and start the next task from step 1.

> **Important:** Each task must branch off your dedicated `TRUNK`, not off a previous task branch.
> 
> **Important:** Create the PR as soon as you are done with the task - do not implement all the tasks and then first create the PR's near the end.

The goal is to implement as many tasks as possible. You decide the quality/performance trade-off.

## If You Run Out of Tasks

Make sure all your completed tasks have open PRs to your `TRUNK` branch. Then close the laptop and wait for the rest of the group to finish. We will do the surveys and focus group when everyone is done or when time is up.

## If You Get Stuck

If you get stuck on a task, skip it and move on to the next one - If you have partially implemented tasks create a `draft` where you write in the description how many subtasks you implemented (e.g. if the task has 5 subtasks and you have implemented 3, create a `draft` with the description "3 subtasks out of 5").

If you ask for help, we might choose not to assist you, as our guidance might bias your solution. If you believe an issue description or a test is broken or not described properly, you may flag it to us — but we cannot help with implementation or describe the task further for you.

## Breaks

You manage your own time. If you need to go to the bathroom or similar, you do not need to ask for permission. You may also talk to the other participants, but **not about the tasks or how to complete them**. Your completion time is recorded, so please be mindful of that.
