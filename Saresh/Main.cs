using fNbt;
using MiNET;
using MiNET.Blocks;
using MiNET.Entities;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET.Utils;
using MiNET.Worlds;
using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace Saresh
{
    [Plugin(Author = "caborambo", Description = "Plugin for basic Schematic utilities.", PluginName = "Saresh", PluginVersion = "v1.0 beta")]
    public class SareshPlugin : Plugin
    {
        public static readonly string CMDPREFIX = ChatColors.DarkGray + "[" + ChatColors.Yellow + "S" + ChatColors.DarkGray + "] " + ChatColors.White;
        public Settings settings = new Settings();

        protected override void OnEnable()
        {
            Utils.WriteConsoleLine("Enabling plugin...");

            if (File.Exists("Schematic/Schematic.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Settings));
                FileStream filestream = new FileStream("Schematic/Schematic.xml", FileMode.Open);
                settings = (Settings)ser.Deserialize(filestream);
                filestream.Close();
            }
            if (!File.Exists("Schematic/Schematic.xml"))
            {
                Utils.WriteConsoleLine("Settings file not found!");
                Utils.WriteConsoleLine("Creating new settings file...");
                File.Create("Schematic/Schematic.xml").Close(); // Literally, creates file and instantly closes it
                Utils.WriteConsoleLine("Settings file created!");
            }
        }

        public override void OnDisable()
        {
            Utils.WriteConsoleLine("Disabling plugin...");
            if (File.Exists("Schematic/Schematic.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Settings));
                TextWriter writer = new StreamWriter("Schematic/Schematic.xml");
                ser.Serialize(writer, settings);
                writer.Close();
            }
        }

        [Command(Command = "/sche")]
        [Command(Command = ".sche")]
        public void Schematic(Player sender, params string[] args)
        {
            string function = args[0].ToLower();

            if (string.IsNullOrEmpty(function))
            {
                sender.SendMessage(CMDPREFIX + ChatColors.Red + "Unknown function.", type: MessageType.Raw);
            }
            if (function.Equals("import"))
            {
                
            }
            if (function.Equals("export"))
            {

            }
            if (function.Equals("list"))
            {

            }
        }

        public bool ImportSchematic(Player requester, Level level, string filepath)
        {
            NbtFile schematic = new NbtFile();
            schematic.LoadFromFile(filepath);

            if (schematic != null)
            {
                NbtTag widthNbtTag = schematic.RootTag["Width"];
                NbtTag heightNbtTag = schematic.RootTag["Height"];
                NbtTag lengthNbtTag = schematic.RootTag["Length"];
                NbtTag materialsNbtTag = schematic.RootTag["Materials"];
                NbtTag blocksNbtTag = schematic.RootTag["Blocks"];
                NbtTag dataNbtTag = schematic.RootTag["Data"];
                NbtTag entitiesNbtTag = schematic.RootTag["Entities"];
                NbtTag tileEntitiesNbtTag = schematic.RootTag["TileEntities"];

                if (widthNbtTag == null)
                {
                    Utils.WriteConsoleLine("Width NBT Tag is null!");
                    requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a Width NBT Tag)");
                    return false;
                }
                if (widthNbtTag != null)
                {
                    if (heightNbtTag == null)
                    {
                        Utils.WriteConsoleLine("Height NBT Tag is null!");
                        requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a Height NBT Tag)");
                        return false;
                    }
                    if (heightNbtTag != null)
                    {
                        if (lengthNbtTag == null)
                        {
                            Utils.WriteConsoleLine("Length NBT Tag is null!");
                            requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a Length NBT Tag)");
                            return false;
                        }
                        if (lengthNbtTag != null)
                        {
                            if (materialsNbtTag == null)
                            {
                                Utils.WriteConsoleLine("Materials NBT Tag is null!");
                                requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a Materials NBT Tag)");
                                return false;
                            }
                            if (materialsNbtTag != null)
                            {
                                if (blocksNbtTag == null)
                                {
                                    Utils.WriteConsoleLine("Blocks NBT Tag is null!");
                                    requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a Blocks NBT Tag)");
                                    return false;
                                }
                                if (blocksNbtTag != null)
                                {
                                    if (dataNbtTag == null)
                                    {
                                        Utils.WriteConsoleLine("Data NBT Tag is null!");
                                        requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a Data NBT Tag)");
                                        return false;
                                    }
                                    if (dataNbtTag != null)
                                    {
                                        if (entitiesNbtTag == null)
                                        {
                                            Utils.WriteConsoleLine("Entities NBT Tag is null!");
                                            requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a Entities NBT Tag)");
                                            return false;
                                        }
                                        if (entitiesNbtTag != null)
                                        {
                                            if (tileEntitiesNbtTag == null)
                                            {
                                                Utils.WriteConsoleLine("Tile Entities NBT Tag is null!");
                                                requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's missing a TileEntities NBT Tag)");
                                                return false;
                                            }
                                            if (tileEntitiesNbtTag != null)
                                            {
                                                if (widthNbtTag is NbtShort)
                                                {
                                                    if (heightNbtTag is NbtShort)
                                                    {
                                                        if (lengthNbtTag is NbtShort)
                                                        {
                                                            if (materialsNbtTag is NbtString)
                                                            {
                                                                if (blocksNbtTag is NbtByteArray)
                                                                {
                                                                    if (dataNbtTag is NbtByteArray)
                                                                    {
                                                                        if (entitiesNbtTag is NbtList)
                                                                        {
                                                                            if (tileEntitiesNbtTag is NbtList)
                                                                            {
                                                                                short width = (widthNbtTag as NbtShort).Value;
                                                                                short height = (heightNbtTag as NbtShort).Value;
                                                                                short length = (lengthNbtTag as NbtShort).Value;
                                                                                string materials = (materialsNbtTag as NbtString).Value;
                                                                                byte[] blocks = (blocksNbtTag as NbtByteArray).Value;
                                                                                byte[] data = (dataNbtTag as NbtByteArray).Value;
                                                                                NbtCompound[] entities = (entitiesNbtTag as NbtList).ToArray() as NbtCompound[];
                                                                                NbtCompound[] tileEntities = (tileEntitiesNbtTag as NbtList).ToArray() as NbtCompound[];

                                                                                var size = height * width * length;

                                                                                if (materials.Equals("Pocket"))
                                                                                {
                                                                                    // Blocks
                                                                                    for (var y = 0; y < height; y++)
                                                                                    {
                                                                                        if (y > 127) break;
                                                                                        for (var z = 0; z < length; z++)
                                                                                        {
                                                                                            for (var x = 0; x < width; x++)
                                                                                            {
                                                                                                var index = y * width * length + z * width + x;
                                                                                                var id = blocks[index] & 0xFF;
                                                                                                var damage = data[index];
                                                                                                if (id != 0)
                                                                                                {
                                                                                                    var tx = x + requester.KnownPosition.X;
                                                                                                    var ty = y + requester.KnownPosition.Y;
                                                                                                    var tz = z + requester.KnownPosition.Z;

                                                                                                    Block block = new Block(Convert.ToByte(id)) { Coordinates = new BlockCoordinates((int)tx, (int)ty, (int)tz), Metadata = damage };
                                                                                                    level.SetBlock(block);
                                                                                                    block.BlockUpdate(level, new BlockCoordinates((int)tx, (int)ty, (int)tz));
                                                                                                    requester.SendMessage("Pasting ... " + ((index / size) * 100) + " %"
                                                                                                    + "\n  # X " + tx + ", Y " + ty + ", Z " + tz + ", Block : " + id + ":" + damage);
                                                                                                    Thread.Sleep(settings.BlockSetDelay);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }

                                                                                    foreach (var entityData in entities)
                                                                                    {
                                                                                        if (entityData.Contains("id"))
                                                                                        {
                                                                                            Entity entity = new Entity(int.Parse(((NbtString)entityData["id"]).Value), requester.Level);
                                                                                        }
                                                                                    }
                                                                                    return true;
                                                                                }
                                                                                if (materials.Equals("Alpha"))
                                                                                {
                                                                                    // Blocks
                                                                                    for (var y = 0; y < height; y++)
                                                                                    {
                                                                                        if (y > 127) break;
                                                                                        for (var z = 0; z < length; z++)
                                                                                        {
                                                                                            for (var x = 0; x < width; x++)
                                                                                            {
                                                                                                var index = y * width * length + z * width + x;
                                                                                                var id = blocks[index] & 0xFF;
                                                                                                var damage = data[index];
                                                                                                if (id != 0)
                                                                                                {
                                                                                                    var tx = x + requester.KnownPosition.X;
                                                                                                    var ty = y + requester.KnownPosition.Y;
                                                                                                    var tz = z + requester.KnownPosition.Z;

                                                                                                    Block block = new Block(Convert.ToByte(id)) { Coordinates = new BlockCoordinates((int)tx, (int)ty, (int)tz), Metadata = damage };
                                                                                                    level.SetBlock(Utils.ConvertBlock(block));
                                                                                                    block.BlockUpdate(level, new BlockCoordinates((int)tx, (int)ty, (int)tz));
                                                                                                    requester.SendMessage("Pasting ... " + ((index / size) * 100) + " %"
                                                                                                    + "\n  # X " + tx + ", Y " + ty + ", Z " + tz + ", Block : " + id + ":" + damage);
                                                                                                    Thread.Sleep(settings.BlockSetDelay);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }

                                                                                    foreach (var entityData in entities)
                                                                                    {
                                                                                        if (entityData.Contains("id"))
                                                                                        {
                                                                                            // I'll follow the MCPC Entity Chunk format
                                                                                            // (http://minecraft.gamepedia.com/Chunk_format#Entity_Format)
                                                                                            // Don't know if MCEdit exports it the same way
                                                                                            // In schematics.
                                                                                            // Hmm.
                                                                                            Entity entity = new Entity(int.Parse(((NbtString)entityData["id"]).Value), requester.Level);
                                                                                        }
                                                                                    }
                                                                                    return true;
                                                                                }
                                                                                if (materials.Equals("Classic"))
                                                                                {
                                                                                    Utils.WriteConsoleLine("Old schematic detected.");
                                                                                    requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: old schematic.");
                                                                                    return false;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                Utils.WriteConsoleLine("TileEntities NBT Tag isn't a NBTList!");
                                                                                requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Data NBT Tag isn't a NBTList)");
                                                                                return false;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Utils.WriteConsoleLine("Entities NBT Tag isn't a NBTList!");
                                                                            requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Entities NBT Tag isn't a NBTList)");
                                                                            return false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Utils.WriteConsoleLine("Data NBT Tag isn't a NBTByteArray!");
                                                                        requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Data NBT Tag isn't a NBTByteArray)");
                                                                        return false;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Utils.WriteConsoleLine("Blocks NBT Tag isn't a NBTByteArray!");
                                                                    requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Blocks NBT Tag isn't a NBTByteArray)");
                                                                    return false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Utils.WriteConsoleLine("Materials NBT Tag isn't a NBTString!");
                                                                requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Materials NBT Tag isn't a NBTString)");
                                                                return false;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Utils.WriteConsoleLine("Length NBT Tag isn't a NBTShort!");
                                                            requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Length NBT Tag isn't a NBTShort)");
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Utils.WriteConsoleLine("Height NBT Tag isn't a NBTShort!");
                                                        requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Height NBT Tag isn't a NBTShort)");
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    Utils.WriteConsoleLine("Width NBT Tag isn't a NBTShort!");
                                                    requester.SendMessage(CMDPREFIX + ChatColors.Red + "Error: malformed schematic file. (Schematic's Width NBT Tag isn't a NBTShort)");
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (schematic == null)
            {
                Utils.WriteConsoleLine("Invalid schematic!");
                Utils.WriteConsoleLine("Cancelling schematic import...");
                return false;
            }
            return false;
        }
    }
}
