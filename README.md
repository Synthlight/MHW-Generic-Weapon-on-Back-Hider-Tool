### MHW Weapon Hider

This is an exe tool that either loops through a given folder for any mod3s found, or just the mod3 directly.

Either run like this:
`MHW-Weapon-Hider.exe {folder with mod3s or a mod3}`
Or just drag'n'drop the folder/mod3 onto the exe.

For folder batch, if a mod3 is in a `\parts\` or `\emblem\` folder, the LOD is set to 0 instead.
The result is all weapon parts are permanently invisible.
This is due to external mod3 parts seemingly ignoring visCon.

This isn't perfect. Some weapons are troublesome like the MR Jho CB which is already visCon 1 for the shield, so the shield becomes completely invisible.
I probably won't add special cases so, if it works? Great. If not? Oh well.

THERE IS NO UI. RUNNING THE EXE BY ITSELF DOES NOTHING.