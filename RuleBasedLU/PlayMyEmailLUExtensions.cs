using System;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace RuleBasedLU;

public static class PlayMyEmailLUExtension
{
    public static bool IsMatch(this PlayMyEmailLU intent, string query, string market)
    {
        return intent
            .GetPatterns(market)
            .Any(pattern => Regex.IsMatch(query, pattern, RegexOptions.IgnoreCase));
    }

    public static (string Domain, string Intent) DomainAndIntent(PlayMyEmailLU intent)
    {
        return intent switch
        {
            PlayMyEmailLU.AcceptMeeting => ("calendar", "accept_meeting"),
            PlayMyEmailLU.AddContact => ("email", "add_contact"),
            PlayMyEmailLU.Archive => ("email", "archive"),
            PlayMyEmailLU.CalendarOther => ("calendar", "calendar_other"),
            PlayMyEmailLU.Cancel => ("email", "cancel"),
            PlayMyEmailLU.ChangeCalendarEntry => ("calendar", "change_calendar_entry"),
            PlayMyEmailLU.CheckAvailability => ("email", "check_availability"),
            PlayMyEmailLU.ClearFlag => ("email", "clear_flag"),
            PlayMyEmailLU.Confirm => ("email", "confirm"),
            PlayMyEmailLU.ConnectToMeeting => ("calendar", "connect_to_meeting"),
            PlayMyEmailLU.ContactMeetingAttendees => ("email", "contact_meeting_attendees"),
            PlayMyEmailLU.ContactSupport => ("feedback", "contact_support"),
            PlayMyEmailLU.CreateCalendarEntry => ("calendar", "create_calendar_entry"),
            PlayMyEmailLU.DeclineMeeting => ("calendar", "decline_meeting"),
            PlayMyEmailLU.Delete => ("email", "delete"),
            PlayMyEmailLU.DeleteCalendarEntry => ("email", "delete_calendar_entry"),
            PlayMyEmailLU.EmailOther => ("email", "email_other"),
            PlayMyEmailLU.Exit => ("email", "exit"),
            PlayMyEmailLU.Fallback => ("email", "other"),
            PlayMyEmailLU.FindCalendarEntry => ("calendar", "find_calendar_entry"),
            PlayMyEmailLU.FindCalendarWhen => ("calendar", "find_calendar_when"),
            PlayMyEmailLU.FindCalendarWhere => ("calendar", "find_calendar_where"),
            PlayMyEmailLU.FindCalendarWho => ("calendar", "find_calendar_who"),
            PlayMyEmailLU.FindCalendarWhy => ("calendar", "find_calendar_why"),
            PlayMyEmailLU.FindDuration => ("calendar", "find_duration"),
            PlayMyEmailLU.FindMeetingRoom => ("calendar", "find_meeting_room"),
            PlayMyEmailLU.Flag => ("email", "flag"),
            PlayMyEmailLU.Forward => ("email", "forward"),
            PlayMyEmailLU.GoBack => ("email", "go_back"),
            PlayMyEmailLU.Help => ("common", "help"),
            PlayMyEmailLU.MarkRead => ("email", "mark_read"),
            PlayMyEmailLU.MarkTentative => ("calendar", "mark_tentative"),
            PlayMyEmailLU.MarkUnRead => ("email", "mark_unread"),
            PlayMyEmailLU.Pause => ("email", "pause"),
            PlayMyEmailLU.Read => ("email", "read"),
            PlayMyEmailLU.Reject => ("email", "reject"),
            PlayMyEmailLU.Repeat => ("email", "repeat"),
            PlayMyEmailLU.Reply => ("email", "reply"),
            PlayMyEmailLU.Resume => ("email", "resume"),
            PlayMyEmailLU.SearchEmail => ("email", "search_email"),
            PlayMyEmailLU.SendEmail => ("email", "send_email"),
            PlayMyEmailLU.ShowNext => ("email", "show_next"),
            PlayMyEmailLU.ShowPrevious => ("email", "show_previous"),
            PlayMyEmailLU.SkipConversation => ("email", "show_next"),
            PlayMyEmailLU.StartOver => ("email", "start_over"),
            PlayMyEmailLU.SubmitFeedback => ("feedback", "submit_feedback"),
            PlayMyEmailLU.TimeRemaining => ("calendar", "time_remaining"),
            PlayMyEmailLU.ChangeEmail => ("email", "change_email"),
            PlayMyEmailLU.FastForward => ("email", "fast_forward"),
            PlayMyEmailLU.MoveEmail => ("email", "move_email"),
            PlayMyEmailLU.SelectNone => ("email", "select_none"),
            PlayMyEmailLU.SwitchAccount => ("email", "switch_account"),
            _ => (string.Empty, string.Empty),
        };
    }

    private static string[] PatternsForEnglish(this PlayMyEmailLU intent)
    {
        return intent switch
        {
            PlayMyEmailLU.AcceptMeeting
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not)\b))(.*\b((?:accept|attend|confirm)(?:ing)?|be there|sign me up|book me in)\b(?:.*\b(meeting|invitation)\b)?)",
                },
            PlayMyEmailLU.Archive
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not)\b)).*\b((?:archive|(?:mark |move to |put in )+archive))\b",
                },
            PlayMyEmailLU.ClearFlag
                => new string[]
                {
                    @"(.*\b(de-flag|unflag|flag removal|do not flag|not flag|don't flag)( it)?\b.*\b(this email|flag)?\b)|\b((clear|remove|take off|stop|undo|eliminate|discontinue|cancel) (the )?flag\b)",
                },
            PlayMyEmailLU.ConnectToMeeting
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not)\b))(.*\b((?:join|start|enter|begin|attend)(?:ing)?|put me|get me|connect me|add me|call|dial)(?: in| on| to| into)?\b.*\b(meeting)?\b)",
                },
            PlayMyEmailLU.DeclineMeeting
                => new string[]
                {
                    @"^(?!.*\b((?:will|shall|can|may))\b)(.*\b((?:cannot make it|won'?t be able to attend|unavailable|not free|won'?t come|can'?t attend|have to decline|pass|busy|have a conflict|other commitments|won'?t join|can'?t join|have to say no|not going|decline|won'?t be there|can'?t be there|have to skip it))\b.*\b(meeting)?\b)",
                },
            PlayMyEmailLU.Delete
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not)\b))(.*\b((?:delete|remove|get rid of|trash|erase|discard))\b.*\b(this)?\b.*\b(email|message)?\b)",
                },
            PlayMyEmailLU.Exit
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not)\b)).*\b((?:exit|quit|stop|end|close|terminate|finish|abort|goodbye|bye|we'?re done|I'?m done|I'?m finished|that'?s all|that'?ll be all|power off|shut down|turn off|see you later|finished))\b",
                },
            PlayMyEmailLU.Flag
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not)\b))(.*\b((?:flag|highlight|set a flag for|mark as important|flag as important|highlight as important))\b.*\b(this)?\b.*\b(email|message)?\b)",
                },
            PlayMyEmailLU.MarkRead
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not|unread|next|previous)\b)).*\b((?:mark(ing)?|set|(?:mark |set )+(?:this email|as read)))\b",
                },
            PlayMyEmailLU.MarkTentative
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|shouldn'?t|won'?t|wouldn'?t|not)\b))(.*\b((?:maybe|might|possibly|potentially|not sure|tentative|try to|unsure|see if I can|perhaps|chance I might|pencil it in))\b.*\b(meeting)?\b)",
                },
            PlayMyEmailLU.MarkUnRead
                => new string[]
                {
                    @"^(?!.*\b(don't|do not|not|can't|cannot|shouldn't|won't|wouldn't|read)\b).*\b(mark(ing)?|set|(?:mark |set )+(?:this email|as unread))\b",
                },
            PlayMyEmailLU.Pause
                => new string[]
                {
                    @"^(?!.*\b(don't|do not|not|can't|cannot|shouldn't|won't|wouldn't)\b).*\b(pause|stop|hold on|wait|freeze|halt|suspend|don't continue|do not proceed)\b",
                },
            PlayMyEmailLU.Reply
                => new string[]
                {
                    @"^(?!.*\b(do'?n't|not|can't|cannot|shouldn't|won't|wouldn't)\b).*\b(can you )?(?=.*\b(reply|respond|answer|write back|response|send (message )?back|type (response|my response)|response to (this )?email|send (an )?email back)\b).*\b(this|email|message)?\b",
                },
            PlayMyEmailLU.Resume
                => new string[]
                {
                    @"^(?!.*\b((?:don'?t|do not|can'?t|cannot|shouldn'?t|won'?t|wouldn'?t|next|previous)\b))(?!.*play my emails from )(?!.*play my emails about )(?!.*play emails from ).*\b((?:resume|continue|start again|play|proceed|carry on|pick up where I left off|keep going))\b",
                },
            PlayMyEmailLU.ShowNext
                => new string[]
                {
                    @"^(?!.*\b(don't|do not|not|can't|cannot|shouldn't|won't|wouldn't)\b)(?!.*\b(account)\b)(?!.*\b(skip this part|skip that part|skip this section|skip that section|skip this table|skip that table|skip this paragraph|skip that paragraph|next part|next section|next paragraph)\b)(.*\b(next|next email|move to the next|show me the next|read the next one|go to the next message|skip to the next|see the next one|open the next|proceed to the next|go forward|read forward|continue to the next|next message|go to next|skip it|next one|move on|go to the next one|skip|skip this one|want to skip|skip one)\b.*\b(email|message|discussion)?\b)",
                },
            PlayMyEmailLU.ShowPrevious
                => new string[]
                {
                    @"^(?!.*\b(don't|do not|not|can't|cannot|shouldn't|won't|wouldn't|account)\b).*\b(previous|go back|read previous email|show me the previous one|move to the email before this|backtrack|read the previous message|go to the previous one)\b",
                },
            PlayMyEmailLU.SkipConversation
                => new string[]
                {
                    @"^(?!.*\b(don't|do not|not|can't|cannot|shouldn't|won't|wouldn't)\b)(?:.*\b(skip to the|see the|open the|proceed to the|continue to the|go to|want to)?\b(next|go to next|skip|skip this|skip one)\b.*\bconversation\b)",
                },
            PlayMyEmailLU.Repeat => new string[] { @"^(repeat|reread|read it again|replay)$", },
            PlayMyEmailLU.ChangeEmail
                => new string[]
                {
                    @"^(?i)(i want to dictate my message again|i want to replace the message|change the message to .+?|change message to .+?|change the subject to .+?|change subject to .+?|change the title to .+?|change title to .+?|change the recipient to .+?|change who it is going to|change who the email is going to|change the title|update the title|edit email|edit in outlook|edit in outlook mobile)$",
                },
            PlayMyEmailLU.Confirm
                => new string[]
                {
                    @"\b(yes|sure|yes, please|absolutely|go ahead|do it|accept|okay|alright|yup|fine|send it)(?!\s+an\s+email)\b"
                },
            PlayMyEmailLU.FastForward
                => new string[]
                {
                    @"^(?i)(fast forward|skip (this|that) (part|section|table|paragraph)|next (part|section|paragraph))$",
                },
            PlayMyEmailLU.Forward
                => new string[]
                {
                    @"^(?i)forward( the email to .+? saying .+?| the email to .+? with message .+?| the conversation| the thread| the discussion)?$",
                },
            PlayMyEmailLU.MoveEmail
                => new string[]
                {
                    @"^(?i)(move( it)? to( folder)? (.+? folder|.+)|put it in .+? folder)$",
                },
            PlayMyEmailLU.Reject
                => new string[]
                {
                    @"^(no|cancel|cancel it|don't send|don't send it|do not send|do not send it)$"
                },
            PlayMyEmailLU.SearchEmail
                => new string[]
                {
                    @"^(play my emails (from .+|about .+)|read( me)? (the .+ emails|emails on .+|from( folder)? .+|the first email)|search email from( folder)? .+|what emails about .+ do I have from .*|do I have any emails.*|are there any emails from my manager.*|what new emails did I get in the last hour.*|what are my new emails.*|do I have new emails.*|check my emails?|what are my new emails.*|do I have new emails.*)$",
                },
            PlayMyEmailLU.SelectNone => new string[] { @"^nothing$" },
            PlayMyEmailLU.SendEmail
                => new string[]
                {
                    @"^(send an email (to )?(myself|my manager|.+?)( about .+ saying .+| using title .+)?|email (me|my manager|myself)|send (.+?) an email)$",
                },
            PlayMyEmailLU.SwitchAccount
                => new string[]
                {
                    @"^(?i)(go to (the (next|first|last|previous) account|my (.+?) account)|play emails from my (next|other|.+?) account|switch to (next|previous) account)$",
                },
            PlayMyEmailLU.CreateCalendarEntry
                => new string[]
                {
                    @"^(?!.*\b(don't|do not|not|can't|cannot|shouldn't|won't|wouldn't)\b)(.*\b(schedule|set up|arrange|plan|organize) ?a? ?(meeting|appointment)\b)"
                },
            PlayMyEmailLU.Help => new string[] { @"^help$" },
            PlayMyEmailLU.SubmitFeedback
                => new string[] { @"(?i)\b(?:Send|I\s+have)?\s*(?:a\s+)?feedback\b" },
            PlayMyEmailLU.ContactSupport => new string[] { @"^contact support$" },
            _ => new string[] { },
        };
    }

    private static string[] PatternsForFrench(this PlayMyEmailLU intent)
    {
        return Array.Empty<string>();
    }

    private static string[] PatternsForSpanish(this PlayMyEmailLU intent)
    {
        return Array.Empty<string>();
    }

    private static string[] PatternsForPortuguese(this PlayMyEmailLU intent)
    {
        return Array.Empty<string>();
    }

    private static string[] GetPatterns(this PlayMyEmailLU intent, string market)
    {
        if (market == null)
        {
            return Array.Empty<string>();
        }

        if (market.ToLower(CultureInfo.CurrentCulture).StartsWith("en"))
        {
            return PatternsForEnglish(intent);
        }

        if (market.ToLower(CultureInfo.CurrentCulture).StartsWith("fr"))
        {
            return PatternsForFrench(intent);
        }

        if (market.ToLower(CultureInfo.CurrentCulture).StartsWith("pt"))
        {
            return PatternsForPortuguese(intent);
        }

        if (market.ToLower(CultureInfo.CurrentCulture).StartsWith("es"))
        {
            return PatternsForSpanish(intent);
        }

        return Array.Empty<string>();
    }
}
