---
title: Handling Merge Conflicts
created: Wednesday 5th February 2025 19:33
Last Modified: Wednesday 5th February 2025 19:33
Aliases:
Tags: quick_note
---

2025-02-05

# Handling Merge Conflicts

---
## Syntax of a Conflict
---
- Suppose you are merging 2 Branches "branchA" and "branchB" and you run into a merging conflict
- "branchB" will often be the "master" or "main" branch and it is the branch that is being merged into
- GitHub will indicate the section of conflict as such:

```
<<<<<<< branchA
	this.data = [];
	for (let i = 0; i < this.rows; i++) {
		this.data[i] = [];
		for (let j = 0; j < this.cols; j++) {
			this.data[i][j] = 0;
		}
	}
=======
	this.data = Array(this.rows).fill()
>>>>>>> branchB
```

- The lines between <<<<<<< branchA and =======
	- indicates the code that is coming from branchA and is in conflict
- The lines between ======= and >>>>>>> branchB
	- indicates the code that exists in branchB and is in conflict

## What to do?
---
- Just select the code that is appropriate and make adjustments if necessary
- Then, delete the guidelines: 
	- <<<<<<< branchA 
	- =======
	- >>>>>>> branchB