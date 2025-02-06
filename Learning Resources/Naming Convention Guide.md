---
title: Naming Convention Guide
created: Wednesday 5th February 2025 18:22
Last Modified: Wednesday 5th February 2025 18:21
aliases: 
tags:
  - programming
  - software_engineering
---

2025-02-05

# Naming Convention Guide
---
### **1. Include Your Name or Username**
Start the branch name with your name or GitHub username to identify who created the branch. For example:
- `john/feature-new-ui`
- `jane/bugfix-login-issue`
- `alice/refactor-inventory-system`

---

### **2. Use a Prefix to Indicate the Purpose**
Add a prefix to describe the type of work being done in the branch. Common prefixes include:
- `feature/`: For new features or functionality.
  - Example: `john/feature/add-payment-gateway`
- `bugfix/`: For fixing bugs.
  - Example: `jane/bugfix/resolve-crash-on-login`
- `hotfix/`: For urgent fixes, typically in production.
  - Example: `alice/hotfix/fix-security-vulnerability`
- `refactor/`: For refactoring code without adding new features.
  - Example: `john/refactor/cleanup-inventory-code`
- `chore/`: For maintenance tasks (e.g., updating dependencies, formatting).
  - Example: `jane/chore/update-unity-version`
- `docs/`: For documentation updates.
  - Example: `alice/docs/update-readme`

---

### **3. Add a Short Description**
Include a brief, descriptive name for the branch that explains what it does. Use hyphens (`-`) or slashes (`/`) to separate words for readability. For example:
- `john/feature/add-enemy-ai`
- `jane/bugfix/fix-missing-textures`
- `alice/refactor/optimize-rendering`

---

### **4. Include a Ticket or Issue Number (Optional)**
If your project uses a task management system (e.g., Jira, Trello, GitHub Issues), include the ticket or issue number in the branch name. For example:
- `john/feature/123-add-shopping-cart`
- `jane/bugfix/456-fix-null-reference`

---

### **5. Keep It Short and Meaningful**
Avoid overly long branch names. Aim for clarity and conciseness. For example:
- Good: `john/feature/add-multiplayer-support`
- Too Long: `john/feature/add-multiplayer-support-with-voice-chat-and-lobby-system`

---

### **6. Avoid Special Characters**
Stick to alphanumeric characters, hyphens (`-`), underscores (`_`), and slashes (`/`). Avoid spaces and special characters like `@`, `#`, `$`, etc.

---

### **7. Use Consistent Conventions**
Ensure all team members follow the same naming conventions to maintain consistency. You can document these conventions in your project’s `README` or a shared document.

---

### **Example Branch Names**
Here are some examples of well-named branches:
- `john/feature/add-save-system`
- `jane/bugfix/fix-ui-scaling-issue`
- `alice/refactor/optimize-physics-engine`
- `john/chore/update-dependencies`
- `jane/docs/update-api-documentation`
- `alice/hotfix/fix-login-crash`

---

### **Auditing Changes**
To audit who made what changes:
1. **Check Commit History**:
   - Use `git log` to see the commit history for a branch.
   - Example: `git log --oneline`
2. **View Branch Details on GitHub**:
   - Go to the repository on GitHub, click on the branch, and view the commit history.
3. **Use Pull Requests**:
   - Require pull requests for merging branches. This allows for code reviews and keeps a record of who made changes and why.

---

By following these conventions, you’ll make it easier for your team to collaborate, avoid conflicts, and track changes effectively.