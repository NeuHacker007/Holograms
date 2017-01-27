Readme for the Cheshire Lip-Sync Component
version 0.7.1
Copyright Mad Wonder 2014

Purpose:
The Cheshire Lip-Sync Unity component is a C# script that was created in
order to speed up the process of creating lip-sync animations in Unity.
Lip-sync animations have always been a very time-consuming asset to
produce. They are also essential for making expressive animations for
characters with spoken dialogue. Cheshire is designed to take timing
data in the form of CSV text files, and create a Unity animation file
from that data. It is designed to work in tandem with the Cheshire
application, which can be downloaded for Windows at
http://www.madwonder.com/cheshire
This application can be used to quickly and efficiently produce the CSV
text files that the Cheshire component utilizes.

Target Unity Version:
Cheshire is designed to work with Unity version 4.3 and higher. It uses
the Sprite component or the Skinned Mesh Renderer component to produce
it's animations. Sprites and Blend Shape support were first made
available in Unity version 4.3. Mad Wonder intends to keep the scipt
up-to-date so that it will be available for all future versions of
Unity.

Installation:
Cheshire is a relatively simple Unity component. To install it, simply
copy the Cheshire folder and its contents into your project's Assets
folder. The Cheshire component will now be available to place on any
Unity game object in the standard fashion. It will be under Scripts >
MadWonder > Cheshire in the Add Component menu.

Application:
Cheshire is a standard Unity component. You can apply it with the normal
methodology. Click on the game object you want to apply it to in either
the 3D window or the scene hierarchy. Click the "Add Component" button
in the object's inspector, or go through the "Component" option in the
main menu. Select "Scripts" then "MadWonder" then "Cheshire." The
Cheshire component will then show up on your game object's inspector
panel.

General Use:
When the component is applied to a game object, it will automatically
check to see if the Sprite component or the Skinned Mesh Renderer
component is already applied to the object. If neither component is
present, the Mouth Shapes fields will remain innactive. If you are
attempting to create an animation for a game object that has one of
those components applied to a child object, you will be able to do that.
Simply select the child object in question from the Select Child field.
It is auto-populated with all child objects that have the Sprite or
Skinned Mesh Renderer components applied.

Once the type of lip-sync animation has been determined, the Mouth
Shapes fields will be active. Before you can create an animation, you
must select a value for all of the different mouth shapes. There are ten
mouth shapes for the Sprite component, and nine for the Skinned Mesh
Renderer component. The "Rest" shape for the Skinned Mesh Renderer is
whatever your 3D model's default state is. Any defined Sprite asset can
be used for the Sprite mouth shapes. The Skinned Mesh Renderer fields
are automatically populated with the names of all the available Blend
Shapes that are a part of the 3D model. Simply click on the field and
select the name of the shape you want to use. You can use the same
Sprite or Blend Shape for multiple Mouth Shapes, but you will get better
animation results if all of the Mouth Shapes are distinct.

Once all of the Mouth Shapes have been assigned, the "Create Animation"
button becomes active. When you click this button, it opens up the
"Create Lip Sync Animation" window. This window allows you to choose the
data file for your animation, an audio asset to play with your
animation, and the type of the animation you want to create. (either
Mecanim Generic or Legacy) If you are creating a 3D model animation you
will also see an Anim Strength field. This field allows you to adjust
to what degree the Blend Shapes are applied, and can go from 1 to 100.
The default value is 80. Using a higher value will push the blend shapes
closer to their full value. Lower values will only push them a little
bit, making for a less exaggerated animation.

The Timing Data field is the only required field in this window. Click
on it and you will open the standard Unity Asset Selection window. You
can also drag-and-drop a text asset into the field if that's how you
prefer to handle the interface. Once you've selected your timing data,
the Create Animation button becomes active.

If you want a specific audio file to play when the lip-sync animation is
played, select an audio clip for the Audio Clip field. The Cheshire
component will add an animation event at the beginning of your
lip-sync animation that will play that specific audio clip. If you want
to handle the audio playback yourself, or for whatever reason want
lip-sync without audio, just leave the Audio Clip field blank. Cheshire
will leave the animation event off if there is no audio clip chosen.

The Animation Type field is to allow the option of creating a Mecanim
animation or a Legacy animation. The default is set to Mecanim, but this
is purely up to the user. Whichever type of animation you are currently
using in your project is the type you want to select. Both work, they
just require different set-up for playing them back. If you're familiar
with Unity's animation system, you will likely know which one you want.

When you click on the Create Animation button in the Create Lip Sync
Animation window, it opens a standard save-asset dialog window. Cheshire
creates standard Unity animation files. Choose the folder in your assets
folder where you want the animation to be saved, and whatever name you
want to save it as. Then click the "Save" button. At this point,
Cheshire does the rest, and you can just sit back and wait for your
animation to be created. It usually only takes a second or two, even for
longer, more complicated animations. You now have a standard Unity
animation file that contains all the complicated animation data for
playing a sophisticated lip-sync animation.
