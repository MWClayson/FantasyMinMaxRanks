using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyMinMaxRanks.api
{
    public class League
    {
        public string name { get; set; }
        public string status { get; set; }
        public Metadata metadata { get; set; }
        public Settings settings { get; set; }
        public string avatar { get; set; }
        public object company_id { get; set; }
        public int shard { get; set; }
        public string season { get; set; }
        public string season_type { get; set; }
        public string sport { get; set; }
        public Scoring_Settings scoring_settings { get; set; }
        public string last_message_id { get; set; }
        public object last_author_avatar { get; set; }
        public string last_author_display_name { get; set; }
        public string last_author_id { get; set; }
        public bool last_author_is_bot { get; set; }
        public object last_message_attachment { get; set; }
        public object last_message_text_map { get; set; }
        public long last_message_time { get; set; }
        public object last_pinned_message_id { get; set; }
        public string draft_id { get; set; }
        public object last_read_id { get; set; }
        public string league_id { get; set; }
        public string previous_league_id { get; set; }
        public string[] roster_positions { get; set; }
        public object group_id { get; set; }
        public object bracket_id { get; set; }
        public object bracket_overrides_id { get; set; }
        public object loser_bracket_id { get; set; }
        public object loser_bracket_overrides_id { get; set; }
        public int total_rosters { get; set; }
    }

    public class Metadata
    {
        public string auto_continue { get; set; }
        public string keeper_deadline { get; set; }
        public string latest_league_winner_roster_id { get; set; }
    }

    public class Settings
    {
        public int best_ball { get; set; }
        public int last_report { get; set; }
        public int waiver_budget { get; set; }
        public int disable_adds { get; set; }
        public int capacity_override { get; set; }
        public int waiver_bid_min { get; set; }
        public int taxi_deadline { get; set; }
        public int draft_rounds { get; set; }
        public int reserve_allow_na { get; set; }
        public int start_week { get; set; }
        public int playoff_seed_type { get; set; }
        public int playoff_teams { get; set; }
        public int veto_votes_needed { get; set; }
        public int squads { get; set; }
        public int num_teams { get; set; }
        public int daily_waivers_hour { get; set; }
        public int playoff_type { get; set; }
        public int taxi_slots { get; set; }
        public int sub_start_time_eligibility { get; set; }
        public int last_scored_leg { get; set; }
        public int daily_waivers_days { get; set; }
        public int playoff_week_start { get; set; }
        public int waiver_clear_days { get; set; }
        public int reserve_allow_doubtful { get; set; }
        public int commissioner_direct_invite { get; set; }
        public int veto_auto_poll { get; set; }
        public int reserve_allow_dnr { get; set; }
        public int taxi_allow_vets { get; set; }
        public int waiver_day_of_week { get; set; }
        public int playoff_round_type { get; set; }
        public int reserve_allow_out { get; set; }
        public int reserve_allow_sus { get; set; }
        public int veto_show_votes { get; set; }
        public int trade_deadline { get; set; }
        public int taxi_years { get; set; }
        public int daily_waivers { get; set; }
        public int disable_trades { get; set; }
        public int pick_trading { get; set; }
        public int type { get; set; }
        public int max_keepers { get; set; }
        public int waiver_type { get; set; }
        public int max_subs { get; set; }
        public int league_average_match { get; set; }
        public int trade_review_days { get; set; }
        public int bench_lock { get; set; }
        public int offseason_adds { get; set; }
        public int leg { get; set; }
        public int reserve_slots { get; set; }
        public int reserve_allow_cov { get; set; }
        public int daily_waivers_last_ran { get; set; }
    }

    public class Scoring_Settings
    {
        public float sack { get; set; }
        public float fgm_40_49 { get; set; }
        public float pass_int { get; set; }
        public float pts_allow_0 { get; set; }
        public float pass_2pt { get; set; }
        public float st_td { get; set; }
        public float rec_td { get; set; }
        public float fgm_30_39 { get; set; }
        public float xpmiss { get; set; }
        public float rush_td { get; set; }
        public float def_pr_td { get; set; }
        public float rec_2pt { get; set; }
        public float st_fum_rec { get; set; }
        public float fgmiss { get; set; }
        public float ff { get; set; }
        public float rec { get; set; }
        public float pts_allow_14_20 { get; set; }
        public float fgm_0_19 { get; set; }
        public float def_kr_td { get; set; }
        public float _int { get; set; }
        public float def_st_fum_rec { get; set; }
        public float fum_lost { get; set; }
        public float pts_allow_1_6 { get; set; }
        public float fgm_20_29 { get; set; }
        public float pts_allow_21_27 { get; set; }
        public float xpm { get; set; }
        public float rush_2pt { get; set; }
        public float fum_rec { get; set; }
        public float def_st_td { get; set; }
        public float fgm_50p { get; set; }
        public float def_td { get; set; }
        public float safe { get; set; }
        public float pass_yd { get; set; }
        public float blk_kick { get; set; }
        public float pass_td { get; set; }
        public float rush_yd { get; set; }
        public float fum { get; set; }
        public float pts_allow_28_34 { get; set; }
        public float pts_allow_35p { get; set; }
        public float fum_rec_td { get; set; }
        public float rec_yd { get; set; }
        public float def_st_ff { get; set; }
        public float pts_allow_7_13 { get; set; }
        public float st_ff { get; set; }
    }
}