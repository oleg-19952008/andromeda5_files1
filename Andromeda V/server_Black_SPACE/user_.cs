using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Sockets;
using TransferableObjects;
namespace BlackSPACE.Server
{
    public partial class user
    {

        public void log(string a)
        {
            System.Console.WriteLine("port " + socket.socket.port + "|||" + a);
        }
        public void Read_call_back(IAsyncResult ar)
        {
            try
            {
        //     log("data= " + Encoding.UTF8.GetString(buffer));
                FromBytes(buffer, 2048);
                var rec_bytes = Socket_.EndReceive(ar);
                if (rec_bytes <= 0)
                {
                    Socket_.Shutdown(SocketShutdown.Both);
                }

                log("bytes - " + rec_bytes);

                //log("a" + a);
                //log("b" + b);
                //log("c" + c);


                //  string text2 = reader.ReadToEnd();

 
         DeserializeInContext(new BinaryReader(ToStream(buffer)));

                Socket_.BeginReceive(buffer, 0, buffer.Length, 0, Read_call_back, this);

          
                //  DeserializeInContext(new BinaryReader(ToBytes(str)));
                //   DeserializeInContext(new BinaryReader(str));
                //     log("aaa2");
                //    TransferablesFramework.SerializeITransferable(new BinaryReader(ToStream(snd));
                //    log("a" + a);
                // log("b" + b);
                // log("c" + c);

                //System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\receive_" + rec_bytes + ".txt", buffer);
                //System.IO.File.AppendAllText(Environment.CurrentDirectory + "\\receive_string_" + rec_bytes + ".txt", Encoding.UTF8.GetString(buffer, 0, rec_bytes));

                //   h(Encoding.UTF8.GetString(buffer, 0, rec_bytes).Replace("\n\0", ""));
                //byte[] s = new byte[2];
                //s[0] = PureUdpClient.CommandReadStaticData;
                //s[1] = PureUdpClient.CommandStartPlay;
                ////   = PureUdpClient.CommandReadStaticData + PureUdpClient.CommandStartPlay;
                //Send(s);
            }
            catch
            {

            }
        }

        public void login()
        {
            var now = DateTime.Now;
            var str = new MemoryStream();
            //CreatePlayerParams pl = new CreatePlayerParams()
            //{
            //    loginID = 1,
            //    shipName = "1",
            //    playId = 1,
            //    raceId = 1,
            //    universeId = 1
            //};
            //TransferablesFramework.SerializeITransferable(str, pl, TransferContext.None);
            //log("aaalogin");
            ////    log(""+Convert.ToInt32(ToBytes(str)));
            //Send(ToBytes(str));
            AuthorizeResult authorizeResult = new AuthorizeResult()
            {
                //
                returnCode = (0),
                    dbId=1,
                    
         //       returnCode = (0),
                id = 1,
                serverVersion = 164,
                loginId = 1,
                universeServerIp = "127.0.0.1",
                universeServerPort = 13900,
                galaxyServerPort = 13900,
                galaxyId = 1,
                isInBase = false,
                madmooId = 1,
                playerId = 1,
                sceneName = "Hydra",
              //  maintenanceEndTime = now.AddSeconds((double)0),
                url_payments = "127.0.0.1",//AuthorizeRequest.SafeReadString(br),
                url_avatar = "127.0.0.1",// AuthorizeRequest.SafeReadString(br),
                url_feedback = "127.0.0.1",//AuthorizeRequest.SafeReadString(br),
                url_logout = "127.0.0.1",// AuthorizeRequest.SafeReadString(br),
                url_recruit = "127.0.0.1",// AuthorizeRequest.SafeReadString(br),
                url_fb = "127.0.0.1",// AuthorizeRequest.SafeReadString(""),
                payments_promotion = false,


            };
            ////AuthorizeRequest authorizeRequest = new AuthorizeRequest()
            ////{
            ////    password = "1",
            ////    username = "1",
            ////    language = "en",
            ////    worldIndex = 1,
            ////    warehouseData = Encoding.UTF8.GetBytes("test")
            ////};
            
                TransferablesFramework.SerializeITransferable(str, authorizeResult, TransferContext.None);
        
     
         
            log("aaalogin");
            //    log(""+Convert.ToInt32(ToBytes(str)));
            Send(ToBytes(str),"1");
        

        }
        public void login2()
        {
            var now = DateTime.Now;
            var str = new MemoryStream();
            //CreatePlayerParams pl = new CreatePlayerParams()
            //{
            //    loginID = 1,
            //    shipName = "1",
            //    playId = 1,
            //    raceId = 1,
            //    universeId = 1
            //};
            //TransferablesFramework.SerializeITransferable(str, pl, TransferContext.None);
            //log("aaalogin");
            ////    log(""+Convert.ToInt32(ToBytes(str)));
            //Send(ToBytes(str));
            //AuthorizeResult authorizeResult = new AuthorizeResult()
            //{
            //    returnCode = (0),
            //    dbId = 1,

            //    id = 1,
            //    serverVersion = 164,
            //    loginId = 1,
            //    universeServerIp = "127.0.0.1",
            //    universeServerPort = 13900,
            //    galaxyServerPort = 13900,
            //    galaxyId = 1,
            //    isInBase = false,
            //    madmooId = 1,
            //    playerId = 1,
            //    sceneName = "Hydra",
            //  //  maintenanceEndTime = now.AddSeconds((double)0),
            //    url_payments = "127.0.0.1",//AuthorizeRequest.SafeReadString(br),
            //    url_avatar = "127.0.0.1",// AuthorizeRequest.SafeReadString(br),
            //    url_feedback = "127.0.0.1",//AuthorizeRequest.SafeReadString(br),
            //    url_logout = "127.0.0.1",// AuthorizeRequest.SafeReadString(br),
            //    url_recruit = "127.0.0.1",// AuthorizeRequest.SafeReadString(br),
            //    url_fb = "127.0.0.1",// AuthorizeRequest.SafeReadString(""),
            //    payments_promotion = false,


            //};
            var e = new ExtractionPoint()
            {
                isOnClientSide = true,ownerFraction = 1 
             
            };
            ////AuthorizeRequest authorizeRequest = new AuthorizeRequest()
            ////{
            ////    password = "1",
            ////    username = "1",
            ////    language = "en",
            ////    worldIndex = 1,
            ////    warehouseData = Encoding.UTF8.GetBytes("test")
            ////};
 
                TransferablesFramework.SerializeITransferable(str, e, TransferContext.GuildOverviewNone);




          //  TransferablesFramework.SerializeITransferable(str, authorizeResult, TransferContext.GuildEpInfoRequest);


            log("aaalogin");
            //    log(""+Convert.ToInt32(ToBytes(str)));
           Send(ToBytes(str), "GuildEpInfoResponse");

        }
        public void ammo()
        {
            var str = new MemoryStream();
            var ammo_ = new AmmoType()
            {
                AccSpeedBonus = 1,
                ammoId = 1,
                amount = 1,
                amountMax = 10,
                assetName = "a",
                damageBonus = 10,
                maxAccBonus = 1,
                name = "seven",
                sortIndex = 1
            };
            TransferablesFramework.SerializeITransferable(str, ammo_, TransferContext.None);
            log("aaalogin");
            //    log(""+Convert.ToInt32(ToBytes(str)));
            Send(ToBytes(str),"6");
            var s = new LevelMap()
            {
                accessLevel = 1,
                broadcastPort = 13903,
                commandListenPort = 13903,
                instanceMapOrigin = 1,
                scenename = "Hydra",
                isCollisionAware = false,
                minimapAssetName ="Hydra",
                collisionsMapStep=1,
                commandListenPortS=13903,
                description="test",
                fraction=1,
                galaxyId=1,
                galaxyKey=1,
                height=200,
                Height=200,
                universeId=1,
                __galaxyId=1,
                widthS=100,
                width=100,
                Width=100,
                reqMinLevel=1,
                reqMaxLevel=56,
                nameUI="123",
                heightS=200,
                minZ=1,
                maxX = 200,
                maxZ=200,
                minX=200,
                isPveMap=true,
                isInstance=true
                
            };
            TransferablesFramework.SerializeITransferable(str, s, TransferContext.None);
            log("aaalogin");
       //     Send(ToBytes(str));
        }
        public void init_launcher()
        {
            var str = new MemoryStream();
            //InitialRequest initialRequest = new InitialRequest()
            //{
            //    launcherVersion = 164,
            //    chosenLang = "en"
            //};    
            var initPack = new InitialPack()
            {
                isNeedNewLauncher = false,
                version = 164,
                translations = { { "en", "English" }, { "ru", "Русский" } },
                languages = { { "en", "English" }, { "ru", "Русский" } }, 
                language = "en"
            };
            TransferablesFramework.SerializeITransferable(str, initPack, TransferContext.None);
            //      log("aaa");
            //    log(""+Convert.ToInt32(ToBytes(str)));
            Send(ToBytes(str),"47");
        }
        public void skills()
        {
            var str = new MemoryStream();
            var bs_skci = new BasicSkillNet()
            {
                Name = "0",
                AssetName = "0",
                Description = "0",
                MaxLvl = (11),
                PlayerLvl = 5,
                CanBuy = true,
                ReqSkill1 = 0,
                ReqSkill2 = 1,
                ReqLvl1 = 0,
                ReqLvl2 = 0,
                Skillpoints = 10,
                Price = 0,
                _2H = 10,
                _CH310CO102H = 10
            }; TransferablesFramework.SerializeITransferable(str, bs_skci, TransferContext.None);
            //      log("aaa");
            //    log(""+Convert.ToInt32(ToBytes(str)));
       //     Send(ToBytes(str));
        }
        public void set_ship()
        {
            var str = new MemoryStream();
            var pl = new PlayerObjectPhysics()
            {
                playerAvatarUrl = "",
                teamNumber = (1),
                playerName = "SeVeN",
                guildTag = "|MS|",
                shipName = "Red_Dragon",
                playerId = 1,
                isAlive = true,
                isShooting = false,
                moveState = (14),
                fractionId = 1,
                destinationX = 0,
                destinationY = 0,
                destinationZ = 0,
                distanceToStop = 0,
                speed = 200,
                rotationState = 0,
                rotationX = 0,
                rotationY = 0,
                rotationZ = 0,
                angularVelocity = 0,
                rotationDone = 1,
                destinationAngle = 0,
                miningMineralNbId = 0,
                timeToFinishMining = 1,
                miningState = 0,
                enteringBaseState = (0),
                enteringBaseDoor = (0),
                enteringBaseId = 0,
                lastVisitedBase = 0,
                timeOfLastCombat = DateTime.Now,
                timeOfSlowEnding = DateTime.Now,
                isSlowedFromAmmo = false,
                //     base.Deserialize(br);
                //  cfg = (ShipConfiguration)TransferablesFramework.DeserializeITransferable(new BinaryReader);
                isImmuneToCrowd = false,
                isStunned = false,
                isDisarmed = false,
                isShocked = false,
                isShortCircuitCaster = false,
                isInStealthMode = false,
                isInParty = false,
                isInControl = false,
                isSpeedBoostActivated = false,
                playerLeague = (PvPLeague)(1),
                inPvPRank = false,
                isGuest = false,
                pvpState = (PvPPlayerState)(1),
                selectedPoPnbId = 0
            };
            TransferablesFramework.SerializeITransferable(str, pl, TransferContext.None);
            log("aaalogin");
            //    log(""+Convert.ToInt32(ToBytes(str)));
         //   cfg_ship();
       //     Send(ToBytes(str));
        }
        public void cfg_ship()
        {
            var str = new MemoryStream();
            var cfg = new ShipConfiguration()
            {
                mapIndex = 1,
                hitPoints = 1,
                hitPointsMax = 1,
                shield = 1,
                shieldMax = 1,
                cargoMax = 1,
                assetName = "12",

                weaponSlots = new WeaponSlot[1],

                skillDamage = 1,
                maxRotationSpeed = 1,
                floatUpSpeed = 1,
                acceleration = 1,
                backAcceleration = 1,
                currentVelocity = 1,
                velocityMax = 1,
                mass = 1,
                distanceToStartDecelerate = 1,
                shipName = "12",
                dmgPercentForPlayer = 1,
                dmgPercentForAlien = 1,
                dmgPercentForAllWeapon = 1,
                dmgPercentBonusForEachLaser = 1,
                dmgPercentBonusForEachPlasma = 1,
                dmgPercentBonusForEachIon = 1,
                dmgFlatBonusForEachLaser = 1,
                dmgFlatBonusForEachPlasma = 1,
                dmgFlatBonusForEachIon = 1,
                laserCooldown = 1,
                plasmaCooldown = 1,
                ionCooldown = 1,
                fasterMining = 1,
                shieldRepairPerSec = 1,
                experienceGain = 1,
                damageReductionItems = 1,
                resilience = 1,
                sellBonus = 1,
                fusionPriceOff = (0),
                ammoCreationBonus = (0),
                epIncomeBonus = (0),
                playerLevel = 1,
                targeting = 1,
                targetingForLaser = 1,
                targetingForPlasma = 1,
                targetingForIon = 1,
                avoidanceMax = 1,
                currentAvoidance = 1,
                speedBoostConsumption = 1,
                maxBoostedSpeed = 1,
                haveLaserDamageFlat = false,
                havePlasmaDamageFlat = false,
                haveIonDamageFlat = false,
                haveLaserDamagePercentage = false,
                havePlasmaDamagePercentage = false,
                haveIonDamagePercentage = false,
                haveTotalDamagePercentage = false,
                haveCorpusFlat = false,
                haveCorpusPercentage = false,
                haveShieldFlat = false,
                haveShieldPercentage = false,
                haveEndurancePercentage = false,
                haveShieldPowerFlat = false,
                haveShieldPowerPercentage = false,
                haveTargetingFlat = false,
                haveTargetingPercentage = false,
                haveAvoidanceFlat = false,
                haveAvoidancePercentage = false
            };
            TransferablesFramework.SerializeITransferable(str, cfg, TransferContext.None);
            log("aaalogin");
            //    log(""+Convert.ToInt32(ToBytes(str)));
            Send(ToBytes(str),"30");
        }
        public static byte[] ToBytes(Stream stream)
        {
            long initialPosition = stream.Position;
            stream.Position = 0;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Position = initialPosition;

            return bytes;
        }
        public   UdpCommHeader FromBytes(byte[] buffer, int length)
        {
            UdpCommHeader udpCommHeader = new UdpCommHeader();
            try
            {
           
                BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer, 0, length));
                log("requestType " + (binaryReader.ReadByte()));
                log("context " + (TransferContext)binaryReader.ReadInt16());
                
                log("packetSeq " + (binaryReader.ReadInt64()));
                //   udpCommHeader.playerId = binaryReader.ReadInt64();
                log("playerId " + binaryReader.ReadInt64());
                log("responseToPacketSeq " + binaryReader.ReadInt64());
                log("data " + TransferablesFramework.DeserializeITransferable(binaryReader));
                //  File.WriteAllBytes(Environment.CurrentDirectory + "\\frome_bytes" + length + ".txt", buffer);
                //UdpCommHeader udpCommHeader = new UdpCommHeader();
                //BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer, 0, length));
                //udpCommHeader.requestType = binaryReader.ReadByte();
                //udpCommHeader.context = (TransferContext)binaryReader.ReadInt16();
                //udpCommHeader.packetSeq = binaryReader.ReadInt64();
                //udpCommHeader.playerId = binaryReader.ReadInt64();
                //udpCommHeader.responseToPacketSeq = binaryReader.ReadInt64();
                //udpCommHeader.data = TransferablesFramework.DeserializeITransferable(binaryReader);
            }
            catch
            {
            }
            return udpCommHeader;
        }
        //     public const byte CommandReadStarterData = 163;
        // public const byte CommandFusillade = 34;
        public static Stream ToStream(byte[] bytes)
        {
            return new MemoryStream(bytes, 0,2048 );
        }
        public static long a;
        public static string b;
        public static string c;
        public static bool in_space = false;
        public static int req = 1;
        public void DeserializeInContext(BinaryReader binaryReader)
        {
         //   var oo2 = Convert.ToInt32(br.ReadByte()); 
          var requestType = (int)(binaryReader.ReadByte());
            var context =  (TransferContext)binaryReader.ReadInt16();
            //var packetSeq = (binaryReader.ReadInt64()); 
            //var playerId = binaryReader.ReadInt64();
            //var responseToPacketSeq = binaryReader.ReadInt64();
           var data = TransferablesFramework.DeserializeITransferable(binaryReader); 

            if (in_space == false)
            {
                init_launcher();
            }
           
    //        if (/* requestType == 1 &&*/context ==TransferContext.GuildEpInfoResponse)
    //        {
                

    ////            in_space = true;

    //            //skills();
    //            //set_ship();
    //        }
            if (requestType == 0 )
            {
              
                login();

                in_space = true;

                //skills();
                //set_ship();
            }
            if (requestType == 163)
            {
                init_launcher();
                login();
            }
            if (requestType == 1)
            {
                login2();
            }
                if (requestType == 6)
            {
             //   ammo();
            }
                if (requestType == 30)
                {
                    cfg_ship();
                }
                //if (in_space == false)
                //{
                //    init_launcher();
                //}
                //if (requestType == 0/*|| requestType == 1)*/)
                //{
                //    login();
                //}
                //if (requestType == 1&&context ==42)
                //{ 

                //    //login2();
                //    //in_space = true;

                //    //skills();
                //    //set_ship();
                //}
                //if (requestType == 6)
                //{
                ////    ammo();
                //}
                ////if (oo2 ==34)
                ////{

                ////}
            }
        public void shr_()
        {
            
            var str = new MemoryStream(); var shr = new ShortAndBool()
            {
                theBool = false,
                theShort = 0
            };
            TransferablesFramework.SerializeITransferable(str, shr, TransferContext.None);



            log("aaalogin");
            //    log(""+Convert.ToInt32(ToBytes(str)));
            Send(ToBytes(str),"хз");
        }
        public void Send(byte[] s,string packet_id)
        {
         
            log("SENT: " + Encoding.UTF8.GetString(s)+"\nPacket id = "+ packet_id);
            try
            {
                if (Socket_ != null && Socket_.Connected)
                {
                    //  Out.WriteLine("To client: " + data, "Packets", ConsoleColor.DarkGreen);
                    // byte[] byteData = Encoding.UTF8.GetBytes(data);
                    Socket_.BeginSend(s, 0, s.Length, 0, SendCallback, Socket_);
                }
            }
            catch { }
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                FromBytes(buffer, 1000000);
                //  int bytesSent = 
                ((Socket)ar.AsyncState).EndSend(ar);
            }
            catch
            {
            }
        }
    }
}
////var s =      TransferablesFramework.DeserializeITransferable(new BinaryReader(ToStream(buffer)));
////     var r = (GenericData)s;
//// log("asdasd"+    r.int1);
////     log("asdds" + r.str1);
//var str = new MemoryStream();
//var s = new SortedList<string, string>() { { "en", "en" } };
//var genericDatum = new InitialPack()
//{
//    // isNeedNewLauncher= false,
//    version = 164,
//    language = "en",

//    translations = { { "en", "English" }, { "ru", "Русский" } },
//    languages = { { "en", "English" }, { "ru", "Русский" } },


//};

////AuthorizeRequest authorizeRequest = new AuthorizeRequest()
////{
////    password = "123",
////    username = "123",
////    language = "en",
////    worldIndex = 1,
////    warehouseData = Encoding.UTF8.GetBytes("test")
////};
//TransferablesFramework.SerializeITransferable(str, genericDatum, TransferContext.None);
//                //      log("aaa");
//                //    log(""+Convert.ToInt32(ToBytes(str)));
//                   Send(ToBytes(str))

//{ "key_ship_type_Mosquito", 0 },
//{ "key_ship_type_Serpent", 1 },
//{ "key_ship_type_Vindicator", 2 },
//{ "key_ship_type_Locust", 3 },
//{ "key_ship_type_Mantis", 4 },
//{ "key_ship_type_Crusader", 5 },
//{ "key_ship_type_Destroyer", 6 },
//{ "key_ship_type_Boar", 7 },
//{ "key_ship_type_Ravager", 8 },
//{ "key_ship_type_Cormorant", 9 },
//{ "key_ship_type_Sentinel", 10 },
//{ "key_ship_type_Vulture", 11 },
//{ "key_ship_type_Red_Dragon", 11 },
//{ "key_ship_type_Tiger", 11 },
//{ "key_ship_type_Nemesis", 11 },
//{ "key_ship_type_Viper", 12 };

//public class TransferablesFramework в нем    public static SortedList<byte, Type> types;
