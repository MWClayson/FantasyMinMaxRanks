using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMinMaxRanks.api.User
{

    public class User
    {
        public string avatar { get; set; }
        public string display_name { get; set; }
        public bool is_bot { get; set; }
        public bool is_owner { get; set; }
        public string league_id { get; set; }
        public Metadata metadata { get; set; }
        public object settings { get; set; }
        public string user_id { get; set; }
    }

    public class Metadata
    {
        public string allow_sms { get; set; }
        public string allow_pn { get; set; }
        public string mascot_item_type_id_leg_13 { get; set; }
        public string team_name_update { get; set; }
        public string join_voice_pn { get; set; }
        public string transaction_free_agent { get; set; }
        public string mascot_item_type_id_leg_17 { get; set; }
        public string mascot_item_type_id_leg_12 { get; set; }
        public string mascot_item_type_id_leg_1 { get; set; }
        public string player_like_pn { get; set; }
        public string mascot_item_type_id_leg_9 { get; set; }
        public string mascot_item_type_id_leg_4 { get; set; }
        public string transaction_commissioner { get; set; }
        public string mascot_item_type_id_leg_3 { get; set; }
        public string mascot_item_type_id_leg_18 { get; set; }
        public string mascot_item_type_id_leg_10 { get; set; }
        public string mascot_item_type_id_leg_5 { get; set; }
        public string mascot_message { get; set; }
        public string trade_block_pn { get; set; }
        public string mascot_item_type_id_leg_7 { get; set; }
        public string mascot_item_type_id_leg_14 { get; set; }
        public string mascot_item_type_id_leg_6 { get; set; }
        public string user_message_pn { get; set; }
        public string mascot_item_type_id_leg_11 { get; set; }
        public string mascot_item_type_id_leg_2 { get; set; }
        public string mention_pn { get; set; }
        public string player_nickname_update { get; set; }
        public string transaction_waiver { get; set; }
        public string mascot_item_type_id_leg_15 { get; set; }
        public string mascot_message_emotion_leg_1 { get; set; }
        public string team_name { get; set; }
        public string mascot_item_type_id_leg_8 { get; set; }
        public string mascot_item_type_id_leg_16 { get; set; }
        public string transaction_trade { get; set; }
        public string avatar { get; set; }
        public string mascot_message_emotion_leg_16 { get; set; }
        public string mascot_message_leg_1 { get; set; }
        public string show_mascots { get; set; }
        public string mascot_message_emotion_leg_14 { get; set; }
        public string mascot_message_emotion_leg_2 { get; set; }
        public string archived { get; set; }
        public string mascot_message_emotion_leg_7 { get; set; }
        public string mascot_message_emotion_leg_8 { get; set; }
        public string mascot_message_emotion_leg_11 { get; set; }
        public string mascot_message_emotion_leg_12 { get; set; }
        public string mascot_message_emotion_leg_3 { get; set; }
        public string mascot_message_emotion_leg_5 { get; set; }
        public string mascot_message_emotion_leg_9 { get; set; }
    }

}
