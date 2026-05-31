# The Office Lockdown

## Game Concept

You are trapped in a locked office after hours. The room is dark and the door is locked. Your goal is to solve five puzzles in sequence to find the key and escape before the timer runs out.

## Puzzle Tasks

1. **Light Switch** — The room is dark. A faintly glowing switch on the wall must be clicked to turn on the lights.
2. **Computer** — With the lights on, click the computer to activate it. The screen displays a visual clue pointing toward the desk drawer.
3. **Desk Drawer** — Click the drawer to slide it open. Inside is a riddle: *"I have hands but cannot clap."*
4. **Briefcase** — Click the briefcase to open a keypad. Type the answer to the riddle to unlock it and reveal the key.
5. **Door** — With the key, click the door to unlock it and escape.

## Clue System

All clues are non-text or indirect:

- The light switch glows faintly in the dark, guiding the player visually.
- The computer screen shows a visual hint toward the drawer, not a direct instruction.
- The riddle in the drawer is indirect — the player must interpret it to find the answer.
- Active puzzle objects highlight yellow when the player looks at them.

## Win and Lose Conditions

- **Win:** All five puzzles solved. The door opens and a "You Escaped!" message appears.
- **Lose:** A three-minute countdown timer reaches zero. A "Time's Up!" message appears.

## Controls

| Input | Action |
|-------|--------|
| W, A, S, D | Move |
| Mouse | Look around |
| Left Click | Interact with objects |

## Asset Sources

| Asset | Source |
|-------|--------|
| Office environment and furniture | Low-Poly Office Set — Unity Asset Store |
| First-person controller | Mini First Person Controller — Unity Asset Store |
| Music | Casual Music Pack — Unity Asset Store |
| Sound effects | Casual Game SFX Pack — Unity Asset Store |

## Scripts

| Script | Purpose |
|--------|---------|
| GameManager.cs | Tracks puzzle progress, runs the countdown timer, updates the UI, handles win and lose states |
| PlayerInteraction.cs | Raycasts from the camera to detect objects, highlights them when the player looks at them, triggers interaction on click |
| ClickableObject.cs | Attached to each puzzle object, handles the specific puzzle logic for each step |
