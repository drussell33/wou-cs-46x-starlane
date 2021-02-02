
# Class workflow

#### Assumptions:

- Official repo has at least main, develop branches (main and dev)
- You have a personal GitHub account
- You are using Terminal or Git Bash
- You forked the official repo to your personal GitHub account
- If you are the maintainer, you do slightly different steps when it comes to PR and End of Sprint

### Create local repo and get latest changes

**Note: Do these commands if you have no local repo already.**

```bash
git clone [Forked Repo URL]
cd [Forked Repo Folder]
git remote add upstream [Official Repo URL]
git checkout dev
git pull upstream dev
```

## Workflow during Sprint

### Make a feature branch

```bash
git checkout dev
git pull upstream dev
git checkout -b [Feature Branch Name]
```

### Commit changes 

```bash
git add -A
git commit -m [Commit Message]
```

### Update local feature branch

```bash
git checkout dev
git pull upstream dev
git checkout [Feature Branch Name]
git merge dev
```

### Push feature branch to your forked repo

**Note: Make sure your changes are committed.**

```bash
git push origin [Branch Name]
```

### Merge feature onto official repo

1. Make pull request on Github
2. When pull requesting, **MERGE FEATURE INTO DEV, NOT MAIN!!!**
3. Tell maintainer of pull request on discord
4. Update local dev branch (even if your pull request was rejected)
5. If PR rejected, fix code or improve feature your working on and repeat these steps until accepted

**After accepted PR and presumably updated local dev branch:**

```bash
git checkout dev
git push origin dev
```

## End of Sprint Workflow

### Sync forked repo to official repo

```bash
cd [Path to Working Directory]
git fetch upstream
git checkout main
git merge upstream/main
git push origin main
```

