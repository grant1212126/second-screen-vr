# second-screen-vr
A VR application designed to test the implications of introducing second screens to users while watching content in VR.

No videos are included in the repository and need to be imported into the application.

# Experiment 

This application corresponds to an experiment to investigate how users interact with second screens presenting auxiliary information in an ARTV viewing scenario. The user is shown four videos on the main screen and an additional video (containing facts about the content) on 3 different types of second screens (one video is shown without a second screen). The application records what the user was looking at during the experiment using 3 bounding boxes representing the main screen, the various side screens, and the background.

# Setup

Scene_02 is included with the repository files with the correct objects instantiated, however, some variables from the scene need to be attatched to the Experiment Control Script in Experiment Controller object need to be manually allocated to their corresponding object/ value. For example, each video for the main screen needs to be imported into Unity and allocated to each element in the "Video Clips" array in the Experiment Controller object. An example setup of the Experiment Control script can be found in the "ExperimentSetup.png" image in the repository.

Project was developed using unity editor version 2021.3.16f1

# Acknowledgements

Assets for the virtual living room were downloaded from the Unity Asset Store: https://assetstore.unity.com/packages/3d/environments/apartment-kit-124055
