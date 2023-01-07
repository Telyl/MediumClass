using MediumClass.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MediumClass.Utilities
{
    class Guids
    {
        private static readonly ModLogger Logger = Logging.GetLogger(nameof(Guids));

        #region Archmage
        internal const string Archmage = "039af4ae-2b4e-44d5-addd-67ba35eae478";
        internal const string ArchmageProgression = "01bf5a3d-0986-4e8d-9a33-13ad8d1213ba";
        internal const string ArchmageSpellbook = "b2e275c3-acfa-4b47-aeac-8163172474de";
        internal const string ArchmageSpellsPerDayTable = "0731b99d-5a6d-47a3-9712-5b7db0c70c8b";
        internal const string ArchmageSpellSlotsTable = "54f3019a-6b8f-40a0-a07a-efd3e372eb81";
        internal const string ArchmageSpiritBonus = "2b9e0b07-2f97-4961-86a9-6a9443797b71";
        internal const string ArchmageSeanceBoon = "90cce36b-6889-4673-9bbd-54085b0ec8e8";
        internal const string ArchmageIntermediate = "f019b7c9-4b35-472f-b842-1352e7908f48";
        internal const string ArchmageIntermediateAbility = "27996019-2a52-450c-b33f-42d18f6d8922";
        internal const string ArchmageIntermediateBuff = "2da103fe-6f2d-4a1c-b16c-cd5a3b651bd5";
        internal const string ArchmageGreater = "b8c01230-86f2-495c-af02-7a20ad8904c3";
        #endregion

        #region Champion
        internal const string Champion = "5f24b820-8644-4d52-aea7-7323b1b2849c";
        internal const string ChampionProgression = "eb13263c-7117-4545-8db9-4fc913596978";
        internal const string ChampionSpiritBonus = "008a7081-8ecd-4f97-8f59-0dcc3a2f41e8";
        internal const string ChampionSeanceBoon = "cad6901e-f6ce-41fc-b822-1105565fc770";
        internal const string ChampionSuddenAttack = "60412b6e-4247-4082-9105-5f0491398ba0";
        internal const string ChampionFleetCharge = "633aa194-4590-448b-85de-b197c4489a85";
        internal const string ChampionFleetChargeAbility = "38de709c-7281-4310-88d0-97c1d96cbdbf";
        internal const string ChampionFleetChargeBuff = "46fda148-2656-4c76-87f2-ad517db05f9e";
        internal const string LegendaryChampion = "2f01ef4d-86e1-4072-8ae9-542d0e00c18a";
        internal const string LegendaryChampionAbility = "a5f5a543-35ff-4647-a63e-8c7af7ff7f46";
        internal const string LegendaryChampionResource = "a15bba3e-b8e2-4174-962a-36be22b11ac8";
        #endregion

        #region Guardian
        internal const string Guardian = "ce5874c4-aef6-4b68-8344-38e2a9970373";
        internal const string GuardianProgression = "90dd3599-8162-43f3-9612-75b4b35bca2c";
        internal const string GuardianSpiritBonus = "c2f636ff-652f-4891-819f-1362a22c90b6";
        internal const string GuardianSeanceBoon = "d5e04574-0761-4265-bbba-dcac35f50057";
        internal const string GuardianAbsorbBlow = "31873197-bad5-42ea-a3b2-a03e662b2fb2";

        internal const string GuardianGreater = "c31441fd-3736-421d-88cd-414d6e5fe8a5";
        internal const string GuardianGreaterAbility = "4f2e4c12-33f6-4d99-9d3b-9b4190bdf5b1";

        internal const string LegendaryGuardian = "b5176b72-a61c-4f18-a5c8-ff17836663d0";
        internal const string LegendaryGuardianAbility = "a181cb9b-d82e-44f4-b78d-8b6bb12537ac";
        internal const string LegendaryGuardianBuff = "bad0f0ca-3a0b-428f-bf1b-dd6eb29bfd37";
        internal const string LegendaryGuardianResource = "74666ee4-5a69-4bdd-a009-779ef8fce86c";
        #endregion

        #region Hierophant
        internal const string Hierophant = "c600360a-ed01-4117-a97a-5c52737a846f";
        internal const string HierophantProgression = "9a87cbe4-ce01-4eda-a5a2-e821d3aef5f8";
        internal const string HierophantSpellbook = "b5aea57f-0587-4a59-a35c-5efa3a730811";
        internal const string HierophantSpellsPerDayTable = "fee2dcbd-6faf-4cdd-a83f-26ccda11e831";
        internal const string HierophantSpellSlotsTable = "654b0f7c-1e3d-406d-a0e9-f0d067348062";
        internal const string HierophantSpellSlotsTableLegendary = "5b9a0727-def6-4209-b0d0-c3dc8e515a1b";
        internal const string HierophantSpiritBonus = "19fc8346-d18c-4356-8287-ada73d3d22c4";
        internal const string HierophantSeanceBoon = "73b487ab-14d4-4a64-979a-0e7eb7e396d7";
        internal const string HierophantEnergyFont = "ec07fe4e-9496-4f5e-941e-957754b2781d";
        internal const string HierophantEnergyFontResource = "ffb78a07-9d23-4bc8-be2d-e56400292b5a";
        internal const string HierophantEnergyFontPositiveHeal = "56815203-d324-42cf-b916-c5004c252f53";
        internal const string HierophantEnergyFontPositiveHarm = "91e92393-d7c4-4596-82d6-0187a7f6f6be";
        internal const string HierophantEnergyFontNegativeHeal = "ca0e7243-df6f-460d-8b0e-9a7351b4c943";
        internal const string HierophantEnergyFontNegativeHarm = "bc9ae899-739a-4ec7-9176-53fcce40b2ba";
        internal const string HierophantOverflowingGrace = "9170839a-ab3f-454b-a3a3-454235653007";
        internal const string HierophantOverflowingGraceSacred = "72092925-9f53-4462-adcd-3a982cc0e7ac";
        internal const string HierophantOverflowingGraceProfane = "14c42dea-aa24-445b-bcd7-86e4c2059318";
        internal const string HierophantSupreme = "5538fb43-9291-4d4d-a6a4-ce567dab268e";
        internal const string HierophantSupremeResource = "afac358c-9d4b-4b5a-a3a2-4403f2457f73";
        #endregion

        #region Marshal
        internal const string Marshal = "fad0e44f-3c96-4653-8179-71a695942668";
        internal const string MarshalProgression = "479074b1-73f3-4510-9fbe-d519ce7d6fb5";
        internal const string MarshalSpiritBonus = "3f1a513b-edb3-4bda-9e33-2b922de9cccc";
        internal const string MarshalSeanceBoon = "86ef441d-ce6b-42ae-93a0-6bb628e33bb9";
        internal const string MarshalSeanceBoonAbility = "1364d5bf-4fb8-44e9-874a-a379d783e403";
        internal const string MarshalSeanceBoonResource = "9cfd03b0-3f76-4591-9be0-4ef8886eb510";

        internal const string MarshalMarshalsOrders = "9bbc9d32-84f1-4be3-877d-5c756d5060b1";
        internal const string MarshalMarshalsOrdersAbility = "b13b604a-d2d6-4d2e-b4e2-7e668b8c4391";

        internal const string MarshalInspiringCallSavingThrowBuff = "10bce4f1-b56f-4f97-9286-f3c4e6c641c5";
        internal const string MarshalInspiringCallAttackBuff = "8af0f950-08d8-4c59-a5b4-58f72e85c333";
        internal const string MarshalInspiringCallAttackAbilitySwift = "dffdae50-34c4-441b-a744-9b3ecd8ebe38";
        internal const string MarshalInspiringCallSavingThrowAbilitySwift = "fe948d6d-51ab-4ed9-8f6a-ba76b4db5ef2";
        internal const string MarshalInspiringCallSwift = "a5420a81-05d3-4fee-ad28-dbe93b0fe04b";
        internal const string MarshalInspiringCallAbilitySwift = "78452bb3-3874-4daf-8385-1607976f4eb2";
        internal const string MarshalInspiringCallAttackAbilityMove = "dd0633e3-0137-4443-92ac-d0b8b8b77811";
        internal const string MarshalInspiringCallSavingThrowAbilityMove = "3e253f3f-c45f-4ed5-8c72-3490cb9e01d6";
        internal const string MarshalInspiringCallMove = "b08efca1-2cf8-425f-9e01-d6972a3d0b9c";
        internal const string MarshalInspiringCallAbilityMove = "6f1b5466-0fed-474b-9f18-8d0aa18bc77e";
        internal const string MarshalInspiringCallAttackAbilityStandard = "8335a949-696f-46be-8f30-bbd3c91277a2";
        internal const string MarshalInspiringCallSavingThrowAbilityStandard = "074c95e8-5b89-4bb9-a446-022ccbaccbf5";
        internal const string MarshalInspiringCallStandard = "14301b62-9160-4397-a292-32d8d1379f04";
        internal const string MarshalInspiringCallAbilityStandard = "c88f941f-8c78-4790-b84b-48f519f4cda0";

        internal const string MarshalDecisiveStrikeStandardBuff = "d1b97daf-e899-4a75-bd68-f5e8fc582b29";
        internal const string MarshalDecisiveStrikeSwiftAbility = "e861c6c8-bd68-4432-93c0-e4e53742b49c";
        internal const string MarshalDecisiveStrikeStandardAbility = "5330612f-3c1b-4433-8a76-cbc87c641e0b";
        internal const string MarshalDecisiveStrikeAbility = "4e306772-c4a9-4c06-9e28-156ccbe54d69";
        internal const string MarshalDecisiveStrikeFeature = "52557a8b-e597-4401-b2eb-3675c716ed43";

        internal const string MarshalLegendaryMarshal = "32dd0799-f2fd-40d4-9530-95510500ccdd";
        internal const string MarshalLegendaryMarshalAbility = "46d392d6-99aa-4a26-9e98-3c07e9304560";
        #endregion

        #region Trickster
        internal const string Trickster = "1959f95e-1fb0-4e4d-8038-af34aeed6ac0";
        internal const string TricksterProgression = "c4d71969-54f2-4c1a-b22f-9adaac088ae7";
        internal const string TricksterSpiritBonus = "2d2a2788-126b-4226-8955-dd1b2a26f73a";
        internal const string TricksterSeanceBoon = "3e0bb85f-c5e3-4b05-a850-4b14670000a8";
        internal const string TricksterSurpriseStrike = "58b988c3-4d54-41d0-801f-fcf0d3643d30";
        internal const string TricksterSurpriseStrikeImpromptu = "38204b3f-48d1-4bf9-902f-3fd9eaac94b3";
        internal const string TricksterSurpriseStrikeCooldownBuff = "1d82eeb2-56f9-4fa0-a58f-391e3e5bb402";

        internal const string TricksterEdgeAthleticsBuff = "34912051-3c19-40cb-a22c-dfcbd9a72c1c";
        internal const string TricksterEdgeMobilityBuff = "31824f30-7302-41a5-a157-fcad6918771b";
        internal const string TricksterEdgeThieveryBuff = "29d12880-5c72-4a37-9b93-6bc0d34a6395";
        internal const string TricksterEdgeStealthBuff = "86c500c8-c47b-4099-a0f4-02b511cc6faa";
        internal const string TricksterEdgeLoreReligionBuff = "039f3495-7507-46ff-a84d-99d491f50f3c";
        internal const string TricksterEdgeLoreNatureBuff = "786358fd-decf-4099-aef5-1fc65d82aaa3";
        internal const string TricksterEdgeKnowledgeArcanaBuff = "ad6ab658-dd88-4779-b582-87d79207f853";
        internal const string TricksterEdgeKnowledgeWorldBuff = "f491621a-6359-4cb5-869a-b683eb4a0267";
        internal const string TricksterEdgePerceptionBuff = "4effd473-bc4e-4ff6-ac1e-53916f2a776a";
        internal const string TricksterEdgePersuasionBuff = "bdd47195-1039-4fa6-af38-ebddfe4059d5";
        internal const string TricksterEdgeUseMagicDeviceBuff = "3202bb09-5ba6-44b2-ab7a-7a4c7e191247";
        internal const string TricksterEdgeAbility = "621bac96-ca8b-4201-a9ba-e3f1b11a7303";
        internal const string TricksterEdge = "9caf0d3f-4a92-432d-a3bf-223f1e1edb7e";
        internal const string TricksterEdgeResource = "b740c592-fd16-40b9-9fa9-2978b3e69166";
        
        internal const string TricksterLegendaryTrickster = "9cd29e04-d51a-427f-98d4-d76743cc0810";
        internal const string TricksterLegendaryTricksterResource = "a23dd292-f23e-4a59-b323-845b38a7bd95";
        internal const string TricksterLegendaryTricksterD20Ability = "d25afd7c-13ae-4c16-a3f6-1ff7ebcbcf6a";
        internal const string TricksterLegendaryTricksterD20Buff = "5694b9f4-0d2c-4678-9079-84945ff09b3d";
        internal const string TricksterLegendaryTricksterPolymorph = "ff2b4377-8a6d-4d93-977d-edf43155dc11";

        internal const string TricksterTransferMagic = "9826e7d3-4fdd-4334-9375-7ad4da567b29";
        internal const string TricksterTransferMagicEffect = "3ea9a03d-4dd4-4e56-b39d-31b8dbcb083e";
        internal const string TricksterTransferMagicAbility = "25966f3c-60de-4efd-b05d-bff180d094c0";

        internal const string TricksterSeanceBoonAbility = "b9a5cbd2-cd37-4dc7-aaee-d6a3f00e2fa8";
        internal const string TricksterSeanceBoonAthleticsBuff = "a606f24e-7f8f-47d8-bc40-3808494c1bf0";
        internal const string TricksterSeanceBoonKnowledgeArcanaBuff = "ad9e06f6-23b8-4623-a807-b455efd7154b";
        internal const string TricksterSeanceBoonKnowledgeWorldBuff = "b63b3c49-e8f6-4b66-a9cd-16d76cb9ed0a";
        internal const string TricksterSeanceBoonLoreNatureBuff = "1f0f9772-b7ec-40d9-b88c-6373e9140449";
        internal const string TricksterSeanceBoonLoreReligionBuff = "380a4a00-4f26-4d89-9a20-8ee1adf85e99";
        internal const string TricksterSeanceBoonMobilityBuff = "ba6b8910-f23e-47f3-959c-06880eb61cde";
        internal const string TricksterSeanceBoonPerceptionBuff = "b5b294bd-5b29-435d-ab55-b762d129e9d8";
        internal const string TricksterSeanceBoonPersuasionBuff = "7f47d5cf-e0e2-4e49-9b00-f920aaef5595";
        internal const string TricksterSeanceBoonResource = "214fd631-cab7-4d5e-bf64-d7c5970fe285";
        internal const string TricksterSeanceBoonStealthBuff = "7f0e1299-869b-4d86-af36-8ee65d4fc310";
        internal const string TricksterSeanceBoonThieveryBuff = "6c962cb9-5972-45fd-a573-90f54838df72";
        internal const string TricksterSeanceBoonUseMagicDeviceBuff = "a4046a02-2a05-4c13-ad89-c11a0d13384b";
        #endregion

        #region Spirit Powers
        internal const string SpiritPowerLesser = "8de7a6b4-a076-4e68-bb45-a397ba4455dc";
        internal const string SpiritPowerIntermediate = "81643114-9cc9-4e8b-a229-32c4839e1bca";
        internal const string SpiritPowerGreater = "0c4d3adc-905b-4e89-8411-9f87a557abb4";
        internal const string SpiritPowerSupreme = "0fa95d9e-9f0b-4565-8117-fbfb5568e15d";
        #endregion

        #region Spirit Surge
        internal const string SpiritSurge = "93328aa1-7653-4fa9-a964-8341e30dd3ee";
        internal const string SpiritSurgeAbility = "3869754d-a331-4000-bf81-82e325f21a75";
        internal const string SpiritSurgeBuff = "9ecf2962-10fb-4641-a086-e7a5aa5267d8";
        #endregion

        #region Medium
        internal const string MediumProficiencies = "a1e16255-434f-4ee2-8c9d-b073fb383b6c";
        internal const string Medium = "b11b2e0b-3076-4b9c-bbf3-ca7f851b5bb4";
        internal const string MediumProgression = "4f18b0de-940d-45d5-8231-ce856686af64";
        internal const string MediumSpellbook = "18218284-8e8e-40c8-b029-adfa64eba37f";
        internal const string MediumSpellsPerDayTable = "da3ba093-bd04-4352-a6a8-d3df0428cc51";
        internal const string MediumSpellsKnownTable = "751377a5-af18-43a6-9559-4faebaa9dd3f";
        internal const string MediumSpellList = "ff3e0346-59b6-4d21-ac57-dc0d9e893bdb";
        internal const string MediumSpiritBonus = "84c93605-a841-4814-b333-f10d8f198020";
        internal const string MediumChannelSpirit = "b9611e74-b3d8-4013-b566-5cb852b5887d";
        internal const string MediumChannelSpiritAbility = "e6bccc02-a590-4197-9813-1cab39b4b754";
        internal const string MediumChannelSpiritAbilityArchmage = "3b3b1e6f-a2de-4f74-8aeb-ebc47a09a4b3";
        internal const string MediumChannelSpiritAbilityArchmageBuff = "5a94d849-02e5-4415-9326-9f950d3b97e8";
        internal const string MediumChannelSpiritAbilityChampion = "44fcfe8f-20f3-4487-abfc-328a08fa462b";
        internal const string MediumChannelSpiritAbilityChampionBuff = "d4faed73-da29-47dc-9e5f-57eccc96746e";
        internal const string MediumChannelSpiritAbilityGuardian = "16fe4899-d2ad-452c-852c-fc55dbcd3dc7";
        internal const string MediumChannelSpiritAbilityGuardianBuff = "895362be-2696-44fa-b4f7-889a5639a4b5";
        internal const string MediumChannelSpiritAbilityHierophant = "e1f1f42c-3b59-4430-ba5e-0eb3f809d586";
        internal const string MediumChannelSpiritAbilityHierophantBuff = "1c91ad43-7fa5-4aac-87dc-51c46eb290ae";
        internal const string MediumChannelSpiritAbilityMarshal = "1651148f-336c-4ea1-90b1-b7a843f81827";
        internal const string MediumChannelSpiritAbilityMarshalBuff = "452415f5-53de-4470-b470-5eb475a5c157";
        internal const string MediumChannelSpiritAbilityTrickster = "f24575e8-30a6-48de-9e71-9745b1c7b935";
        internal const string MediumChannelSpiritAbilityTricksterBuff = "ff1cb998-b9c1-4771-b8a5-101a9ab98c94";
        internal const string MediumSharedSeance = "ca90a9b4-2af6-4c0d-b706-b8f82057302f";
        internal const string MediumSharedSeanceBuff = "a9fb5ebb-77ed-49eb-9b5d-f945ea9b0603";
        internal const string MediumSharedSeanceAbility = "4255013c-64e7-4ae1-ad80-dd8892195983";
        internal const string MediumSharedSeanceResource = "0647bf61-7a73-4a88-adf5-3453209e6f0c";
        internal const string MediumInfluence = "b37c5612-7f17-453e-a8dc-fd61d9060281";
        internal const string MediumInfluenceDebuff = "7e8990eb-8ed1-4625-a695-8ded5b6e166c";
        internal const string MediumInfluenceResource = "8326af03-a110-4071-bce3-26b601c1c28d";
        internal const string MediumPropitation = "a18d34f6-3f97-4c1a-b58c-6c62d099e7f2";
        internal const string MediumSpiritMastery = "7f43ca94-7b10-4660-8f38-09ae8b0e1773";
        internal const string MediumTranceOfThree = "5f6fc22c-018a-45ae-a334-0c4811968e45";
        internal const string MediumTranceOfThreeAbility = "e90c47be-11f1-4702-9cc0-b50bd1775566";
        internal const string MediumTranceOfThreeArchmageBuff = "f766ba20-5018-4d78-8e9b-44b006115769";
        internal const string MediumTranceOfThreeArchmageAbility = "508d30e8-acb3-4d02-8c74-8f7f752c2327";
        internal const string MediumTranceOfThreeChampionBuff = "a7e5094b-fe23-4112-9c14-98594ff9a1c1";
        internal const string MediumTranceOfThreeChampionAbility = "8bce9f61-7990-43e3-bd7c-1e8d6a60e9a3";
        internal const string MediumTranceOfThreeGuardianBuff = "6047d740-455a-46f5-a6dc-b77b11058dd9";
        internal const string MediumTranceOfThreeGuardianAbility = "b73d1af0-08ac-4315-8bea-42dc59d2bed7";
        internal const string MediumTranceOfThreeHierophantBuff = "405fde76-a13a-4f1a-a647-7ef022b10bb3";
        internal const string MediumTranceOfThreeHierophantAbility = "f79f524b-1d37-4ae3-9ae4-60f4264bcaca";
        internal const string MediumTranceOfThreeMarshalBuff = "0b5cb872-310b-4dd3-8afe-876bad06bb15";
        internal const string MediumTranceOfThreeMarshalAbility = "31c65335-fca1-458f-b48c-e29d158ea642";
        internal const string MediumTranceOfThreeTricksterBuff = "c3678338-f30b-44c1-a559-851bd324d338";
        internal const string MediumTranceOfThreeTricksterAbility = "073a383a-1719-4d2b-a694-dd24aa1e13a5";
        internal const string MediumSpellcasterFeat = "6afa9523-eb95-490d-96ad-b6b5e7b37015";
        internal const string MediumSpellcasterFeatProhibitArchmage = "041eb0b7-848b-4e74-8c76-fb5469a7d87e";
        internal const string MediumSpellcasterFeatProhibitHierophant = "92d5cbd0-b6f7-4364-8c63-6c3227377576";
        #endregion

        internal static readonly (string guid, string displayName)[] Spirits =
          new (string, string)[]
          {
              (Archmage, MediumClass.Medium.Spirits.Archmage.Archmage.ArchmageName)
          };

        public static string GuardianSpiritPowers { get; internal set; }
    }
}
