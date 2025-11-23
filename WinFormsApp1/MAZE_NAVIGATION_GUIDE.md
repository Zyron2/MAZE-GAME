# ??? New Maze Layout Guide

## Visual Maze Map (ASCII Art)

```
MAZE DIMENSIONS: 51 × 21 cells
WALKABLE AREA: ~400 cells
WALLS: ~670 cells
MONSTERS: 4
EXIT LOCATION: Right side, middle height
```

---

## Detailed Maze Structure

### Vertical Corridors (Main Arteries)
```
X=8:   Left vertical corridor connecting top and middle
       Running from y=2 to y=11 (10 cells tall)

X=25:  Center vertical corridor (FULL HEIGHT)
       Running from y=2 to y=19 (17 cells tall)
       **Critical path - Monster 4 guards here!**

X=42:  Right vertical corridor
       Running from y=5 to y=18 (13 cells tall)
       **Monster 3 guards this area!**
```

### Horizontal Corridors (Main Pathways)
```
Y=3:   TOP CORRIDOR (Fast route)
       X from 2 to 49 (major highway)
       Direct path but exposed
       **Monster 1 operates near here!**

Y=10:  MIDDLE CORRIDOR (Most direct)
       X from 2 to 49 (full length)
       **MOST DANGEROUS - Monster 1 & Monster 4 here!**

Y=17:  BOTTOM CORRIDOR (Longest route)
       X from 2 to 49 (full length)
       Safer but time-consuming
```

---

## Chamber Areas (Open Spaces)

### Left Chamber
```
Location: X=2-6, Y=4-8 (5×5 area)
Purpose: Open room for navigation
Walls: Minimal internal walls
Exits: North (to top corridor), South (to middle)
Danger: Low-Medium (Monster 2 nearby)
```

### Top-Right Chamber
```
Location: X=35-44, Y=2-6 (9×5 area)
Purpose: Spacious alternate route
Walls: Strategic obstacles at (38,3), (40,4), (42,5)
Exits: West (to center), South (to middle)
Danger: Medium (Monster 3 guards entry)
```

### Bottom-Left Chamber
```
Location: X=10-19, Y=13-17 (9×5 area)
Purpose: Alternative path south
Walls: Obstacles at (12,15), (14,14), (16,16)
Exits: North (to middle), East (to bottom corridor)
Danger: Low-Medium
```

### Bottom-Right Chamber
```
Location: X=35-47, Y=12-18 (12×7 area - LARGEST!)
Purpose: Major bypass area
Walls: Obstacles at (40,14), (45,16)
Exits: North (to middle), East (to right corridor), West (to bottom corridor)
Danger: Low-Medium (Monster 3 may guard)
```

---

## Monster Locations & Coverage

### Monster 1
```
Position: (25, 10)
Location: CENTER of middle corridor
Coverage: Controls vertical & horizontal intersections
Threat Level: HIGHEST
AI: BFS (optimal pathfinding)
Escape Options: Use chambers to bypass
```

### Monster 2
```
Position: (10, 6)
Location: Left-middle junction
Coverage: Guards left chamber exit
Threat Level: HIGH
AI: BFS (optimal pathfinding)
Escape Options: Use top corridor or bottom paths
```

### Monster 3
```
Position: (42, 12)
Location: Right corridor-chamber junction
Coverage: Controls right-side routes
Threat Level: MEDIUM-HIGH
AI: BFS (optimal pathfinding)
Escape Options: Use center vertical or bypass west
```

### Monster 4
```
Position: (30, 10)
Location: CENTER vertical corridor
Coverage: Just east of Monster 1, controls junction
Threat Level: HIGH
AI: BFS (optimal pathfinding)
Note: Creates "net" with Monster 1 at middle corridor
Escape Options: Use chamber detours
```

---

## Winning Route Analysis

### Route A: Upper Path (Fastest)
```
Start: (2, 3)
Path: ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ? ?
       TOP CORRIDOR (y=3) across full width
Distance: ~47 cells
Time: 15-20 seconds
Danger: ????? (monsters active in top areas)
Success Rate: Medium (risky!)

Segments:
  1. Move right along y=3
  2. Avoid or rush past Monster 1 area
  3. Continue to exit at (48, 10)
```

### Route B: Chamber Bypass (Safest)
```
Start: (2, 3)
Path: (2,3) ? (2,6) ? chamber ? (10,17) ? (28,17) ? (48,10)
      Left chamber ? Bottom chamber ? Bottom corridor ? Exit
Distance: ~65 cells
Time: 30-40 seconds
Danger: ????? (monsters mostly elsewhere)
Success Rate: High (safe!)

Key chambers:
  1. Left chamber (5×5 open space)
  2. Bottom-left chamber (9×5 open space)
  3. Bottom corridor (y=17) to bottom-right chamber
  4. Exit corridor to (48, 10)
```

### Route C: Middle-Right Path (Balanced)
```
Start: (2, 3)
Path: (2,3) ? (25,3) ? (25,10) ? (42,10) ? (48,10)
      Top corridor ? Center ? Right corridor ? Exit
Distance: ~48 cells
Time: 20-30 seconds
Danger: ????? (medium-high due to Monster 1)
Success Rate: Medium-High

Key decision points:
  1. When to go down center corridor
  2. Time monster crossing
  3. Reach exit before right-side monsters catch you
```

### Route D: Zigzag Through Chambers
```
Start: (2, 3)
Path: Multiple turns through chambers
      Left ? Bottom-Left ? Center ? Bottom-Right ? Exit
Distance: ~60 cells
Time: 25-35 seconds
Danger: ????? (moderate)
Success Rate: Medium-High

Advantages:
  - Flexible routing
  - Can avoid specific monsters
  - Multiple escape options
```

---

## Passage Connections (Narrow Routes)

### Upper Connector
```
Location: X=20-22, Y=3
Purpose: Connects upper left to center
Length: 3 cells wide
Danger: Medium (Monster 1 may patrol)
Alternative: Use chambers instead
```

### Middle Connector
```
Location: X=30, Y=8-11
Purpose: Connects left-middle to right
Length: 4 cells tall (one-way passage)
Danger: HIGH (Monster 4 here!)
Alternative: Use center vertical or chambers
```

### Lower Connector
```
Location: X=28-30, Y=17
Purpose: Connects bottom areas
Length: 3 cells wide
Danger: Low (far from monsters)
Advantage: Safe transition route
```

---

## Dead Ends & Traps (Avoid These!)

```
Strategic dead ends to avoid:
  - Corner chambers (can trap you)
  - Blocked passages
  - Monster-guarded junctions
  
Design note: All chambers have multiple exits
            (no true dead ends in main areas)
```

---

## Optimal Decision Tree

```
START at (2, 3)
   ?
   ?? Go UP (Top corridor)?
   ?  ?? Fast but risky
   ?     ?? Danger: Monster 1, Monster 4 active
   ?
   ?? Go DOWN (Bottom corridor)?
   ?  ?? Safe but slow
   ?     ?? Use chambers for safety
   ?
   ?? Go CENTER (Via center vertical)?
      ?? Medium risk/reward
         ?? Careful timing needed!
```

---

## Difficulty Zones

### Very High Danger (Avoid if Possible)
- (25, 10) - Monster 1 position
- (30, 10) - Monster 4 position  
- Direct line between them

### High Danger (Risky)
- (42, 12) area - Monster 3
- (10, 6) area - Monster 2
- Y=3 and Y=10 corridors (main paths)

### Medium Danger (Moderate)
- Chamber passages
- Connector corridors
- Areas with multiple exits

### Low Danger (Safest)
- Bottom chambers
- Y=17 corridor (far from monster cluster)
- Left-side areas away from Monster 2

---

## Player Statistics Per Route

### Speed Challenge
**Fastest Route**: Upper path (15s) - High risk
**Safest Route**: Chamber bypass (40s) - Low risk
**Balanced Route**: Middle-right (25s) - Medium risk

### Monster Intelligence
- **BFS Monsters**: Will hunt intelligently through corridors
- **DFS Monsters**: Will explore systematically (if any)
- **Group Strategy**: Monsters create zones of control

### Game Dynamics
- **First 10 seconds**: Reach y=10 corridor junction
- **10-20 seconds**: Major path decision (where to go)
- **20-30 seconds**: Navigate around monsters
- **30+ seconds**: Final approach to exit (low danger)

---

## Navigation Tips

1. **Scout the routes** - First playthrough, explore slowly
2. **Identify monster patterns** - BFS paths are predictable
3. **Use chambers** - Open spaces provide cover
4. **Time your moves** - Wait for monsters to pass
5. **Have a backup** - Know 2+ routes before committing
6. **Bottom is safest** - Y?15 has fewer monsters
7. **Direct is risky** - Center vertical is dangerous

---

## Technical Map Data

```
Total Cells:        1,071 (51 × 21)
Walkable Cells:     ~400 (37%)
Wall Cells:         ~670 (63%)

Graph Statistics:
- Nodes:            ~400
- Edges:            ~1,200 (avg 3 neighbors/cell)
- Longest path:     ~90 cells

Monster Coverage:
- Monster 1:        ~5 cell radius
- Monster 2:        ~4 cell radius
- Monster 3:        ~5 cell radius
- Monster 4:        ~5 cell radius
- Total coverage:   ~60% of critical areas
```

---

## Map Legend

```
# = Wall (impassable)
  = Walkable space
P = Player start (2, 3)
E = Exit goal (48, 10)
M = Monster location

? = Major corridor
? = Major vertical passage
? = Major junction
```

---

## Conclusion

The new 51×21 maze provides:
- ? Multiple strategic routes (4+ distinct paths)
- ? Varying difficulty levels per path
- ? Intelligent monster placement
- ? Replay value and strategy depth
- ? Clear but challenging gameplay
- ? Room for skill development

**Good luck navigating! ??**
