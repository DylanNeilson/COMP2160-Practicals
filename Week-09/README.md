# COMP2160 Game Development - Prac Week 09

## Topics covered:
* Working with meshes and rigs
* Animations and Animators
* Procedural Animation (bonus!)
* Copyright and IP

## Discussion: Copyright and IP
[From J. Zagal _(Contrived) Game Dev Dilemmas (Often) Straight from the Headlines (v1.0)_]

You’re the CEO of a game company that is struggling financially. One day, your team approaches you with an idea – they’ve been studying a recent hit mobile game with novel game mechanics called _Twees!_. This game was recently released in Japan by a secretive game company with no publicly available contact information and it has reportedly been making money hand over fist. _Twees!_ has not been released in Australia. 

Your team convinces you that they can re-create the game, from scratch and without using any computer code or art assets from _Twees!_, in just a few weeks. This new game, which they’ve called _TweentyFortyAte_, would have completely different assets (visual look and design, sound effects, music, text, etc.) but its gameplay and mechanics are identical to those in _Twees!_. 

You are pretty confident that the game would be a hit and could help save your company. You’ve consulted with your in-house counsel and they are confident that releasing _TweentyFortyAte_ would not be problematic in terms of copyright infringement.  Is it ethical for you to release _TweentyFortyAte_?

## Today's Task
Today you will be learning how to work with animations and art assets in Unity to create a rhythm game: https://uncanny-machines.itch.io/comp2160-week-09-sample

The four pillars on screen each have an arrow, corresponding to the keyboard buttons (and the WASD keys). The player must press the button when the corresponding pillar is raised, with a timer counting down each time. The player's avatar, Unitychan, changes dance moves based on the button pressed. As the player increases their score, the lights grow bigger and brighter. The small, pixel art version of Unitychan will change animations if the player keeps up a streak of hit notes.

## Task 1 - Get Familiar With the Project
Spend some time getting familiar with the project and how it works. There are a few scripts and animations in action already:

```GameManager```: Controls the logic of the game, including the time between a new move being presented to the player, the score and streak, as well as reading the input and whether they pressed the right button or not.

```MoveDisplay```: Reads the current move from the game manager and raisers/lowers the pillars.

```UIManager```: Reads the player's score, streak and whether they hit the move or not and displays it on screen.

```MiniChanScript```: Reads the player's current streak and updates the animiation accordingly.

Importantly, game logic is seperate from animations. Game logic should inform animations, but not the other way around.

Have a look through the code and get familiar with how these scripts work, especially ```GameManager``` and  ```MiniChanScript```. This will give you clues for the rest of the prac.

## Task 2 - Importing Models
Firstly, let's import a model to use as our player avatar. We've created a simplified version of the Unity Chan package from Unity Technologies Japan, which you can find on iLearn. Download this package and import it into your project.

In this unit, you will not be expected to create 3D models or rig them up. But it's important to learn how they work. Select the unitychan model (UnityChan2160 > Unity -chan! Model > Art > Models) and take a look at it in the insepctor. If you are working on the lab computers, you should be able to double-click the model to open it in 3D model viewer, where you can observe the mesh without any texturing.

### Rigs
Click on the "Rig" tab to examine the "skeleton" of the model. Rigs allow us to apply different meshes to characters and preserve the overall structure and animations. Press "Configure..." to open up the rig and see what components are mapped to what bone.

![An image of the Unitychan model in the inspector, with the Rig tab selected](images/week9_riginspector.png)

When you are sufficiently traumatized, press "Done" to return to the normal inspector view.

### Git LFS
the Unitychan model is an .FBX file. This is a binary file, and so we don't want to store this using Github's normal version control system. Github provides Git LFS, or Git Large File Storage, which we can use to store this and other similar files.

Copy the .gitattributes file from iLearn and place it inside your repository. If you already have a .gitattributes file, copy the text from the iLearn file into it.

Much like .gitignore file removes certain files from your version control completely, Git LFS places them (models, images, sound, etc) into a seperate form of storage. There is a limit on this, so don't think this gives you license to import whatever you want.

With the Unitychan model and the .gitattributes in your project, save, commit and push your work. Check that the Unitychan model is being correctly stored by navigating to it on Github. You should see something like this:

![An image of the Github repo, with the unitychan.fbx file stored with Git LFS. A notification reading "Stored with Git LFS" confirms this.](images/week9_LFS.png)

## Task 3 - Hook-up Animations
Place the Unitychan model in the scene, somewhere in the centre of the camera's view and rename them "Player". We have provided you with four animations, located in the "Dances" folder. These animations have already been rigged to work with the Unitychan model.

Take a look in the insepctor of your Player object. There is already an "Animator" component, which has "unitychanAvatar" assigned under "Avatar" However, we need to add a Controller. Press Assets > Create > Animator Controller and give it a meaningful name. Assign this controller to the Animator component on your Player.

Animations sometimes make our objects move about in ways we don't want. To minimise this, ensure "Use Root Motion" is ticked. It should all look something like this:

![An image of the completed animator in the inspector for UnityChan, with Apply Root Motion turned on](images/week9_animatorinspector.png)

### Assigning animations
You will remember from COMP1150 connecting animations in an animator. So, open the Player's animator in the Animator panel and drag-and-drop the four animations from the Dance folder. Set the "Idle" animation as the default state, and press play to see how it all looks.

Create transitions for each animation from the "Any State" state, to allow us to switch between animations no matter what. You may also want to untick the "Can Transition to Self" box in the "Settings" of the transition in the inspector. You can preview how the transitions between different moves will look by dragging up the "Preview" tab in the Inspector. Try modifying the Transition duration to get some smooth, but quick transitions. To see the transition between different moves, you can change the "Preview source state".

![An image of the Inspector with the transition selected, showing a preview of UnityChan dancing ."Can Transition To Self" having been unticked.](images/week9_transitionpreview.png)

Your completed animator should look something like this:

![An image of the hooked-up animator, with all states being linked to the "Any State" node.](images/week9_statemachine.png)

### The animator component
Now that we have our animations created, it's time to control the transitions between them through code.

Unity's animator allows the creation of bools, float and integers that can be used to determine transitions between animations called <b>paramaters</b>. You will remember having used them in COMP1150. If you don't, you can revise your notes from that class before moving on (remember, you can still access it on iLearn!).

Add an Int parameter called "Dance Move" to your animator, and then set transitions to each of your moves based on this number as follows (Yes, we're missing number 3. We'll get there!):

* Dance Move Equal to 1 - > Left Animation
* Dance Move Equal to 2 - > Up Animation
* Dance Move Equal to 4 - > Right Animation

### Checkpoint! Save, commit and push your work now

Create a new script called ```Dance``` for your Player. Following the ethos of seperating game logic from animations, this script will ONLY read input and change the animation on the player character. As such, it will need to define the animator:

```
private Animator animator;
```

Add the usual definitions for the input action asset and each input action, as well as the usual ```Awake()```, ```OnEnable()``` and ```OnDisable()``` operations for each input action.

In our ```Start()``` method, we need to reference our Animator component:

```
animator = GetComponent<Animator>();
```

We now want to bring our input and animator together by subscribing events to our dance move inputs. We want to subscribe a new method, ```DoFirstMove()```, to the first move button being pressed, adding the following to our ```Start()``` method:

```
firstMove.performed += DoFirstMove;
```

In this event, all we want to do is call our Animator and set the "Dance Move" integer we defined to the corresponding dance move. In this case, 1:

```
void DoFirstMove(InputAction.CallbackContext context)
{
    animator.SetInteger("Dance Move", 1);
}
```

Have a go at doing the same for the second and fourth moves, following these same steps. Test your animations in game and make any tweaks necessary to get them feeling good.

## To receive half marks, show your tutor:
* Your imported Unitychan model.
* Your animator, and the code that controls transitions.
* Your use of Git LFS.

## Task 4 - Add a new animation!
Our game is missing an animation for the "down" button (dance move no. 3). To download an appropriate animation, head over to https://www.mixamo.com/ and create a new account.


### Uploading your model
Once you're logged-in to Mixamo, you will see a big stream of animations, as well as a preview model. We want to have these animations as ready-to-go for our Unitychan model as possible, so we are going to upload it for the preview. Press "Upload Character" and select the unitychan.fbx from your project. 

Once it is uploaded, you should see a preview window like this:

![An image of the Mixamo preview window, with an untextured UnityChan.](images/week9_autorigger.png)

For the most part, the model hooks up well, but Mixamo doesn't quite know what to do with her eyelashes. We won't worry about that for now, and instead hit "Next".

### Finding an animation
You can now search through the avaliable animations to find one that you think suits the "Down" button dance move. Depending on your previous experience with this kind of thing, we recommend not picking anything that has the character crouch down too much, as this can be a little harder to appropriately set-up in Unity.

Note: Mixamo is pretty resource intensive, and can get a little buggy. If you find the animations aren't loading, try just re-loading the page.

Once you've found an animation you are happy with, press "Download". Set the Format to "FBX for Unity(.fbx)" and the Skin option to "Without Skin", as we don't want to duplicate our model. Then hit "Download".

![An image of the Mixamo download settings, with "FBX for Unity(.fbx) and "Without skin" set.](images/week9_downloadmixamo.png)

### Rigging
Import your new animation into your project, and give it a meaningful name ("Down" will keep things consistent).

We now need to assign UnityChan's rig to the Animation before we can use it. In the inspector for your new animation, click on the Rig tab and set "Animation Type" to Humanoid. For Avatar Definition, we want to create the avatar using the Unitychan model, so select "Copy from another Avatar" and select unityChanAvatar, then press Apply.

![An image of the Rig settings for the new animation, set to "Humanoid" and the unitychanAvatar.](images/week9_animationrigging.png)

### Tweaking animations
There's a few more settings we need to tweak for our animation to work nicely. Select the "Animation" tab and again play the preview of the animation. To change the model to unityChan, press the small mannequin image and select "Other" then find the UnityChan model. Pressing play, you should now see UnityChan running the animation, but she might be stuck in the ground.

Tick the "Loop Time" box to make the animation loop when it ends.

Set the Rotation, Transform and Scale error values to 0. It's important to keep our animation as precise as possible, without changing the rest of the object's transform.

Change the name of the animation from "mixamo.com" in the inspector to its actual name.Then, try to orient UnityChan so she is standing on the ground when doing this dance. Set the "Root Transform Position (Y)" so she is standing on the ground. This will vary depending on your animation, but will sit somewhere between -0.5 and -1.0, give or take. You also may want to set "Based Upon" to "Feet", and tick the "Bake Into Pose" box. 

Once you start testing your animation in the game, you may want to come back to this and tweak the "Root Transform Rotation" and "Root Transform Position (XY)".

![An image of the inspector for the new animation.](images/week9_newanimation.png)

### Checkpoint! Save, commit and push your work now

### Adding to your animator
Now your animation is ready to go! Add it to your animator and ```Dance``` script the same way you did the others.

### Extracting materials
Models like FBX files come with embedded materials, but we may wish to modify them. To do so, select the Unity chan model and navigate to the Materials tab in the inspector. Change location to "Use Embedded Materials". You can now add your own materials into these slots if you wish, or select these materials and modify them. Try changing Unitychan's hair colour by selecting the material and modifying the main and shadow colors.

![An image of the inspector with the material tab selected for the model, with Use Embedded Materials turned on](images/week9_modelsettings.png)


## To receive full marks, show your tutor:
* Your new animation, hooked up and running in the game.


