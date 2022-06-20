﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum CompanySettingType
    {
        CrmApiUsername,
        CrmApiPassword,
        PortalId,
        CrmApiToken,
        CrmApiSalt,
        CrmApiCredentialsVerifiedDate,
        CrmUrlOverride,
        CrmOAuthEndpointRootOverride,
        CrmBaseUrl,
        CrmAuthToken,
        DynamicsViewPrefixFilter,
        ReferralsEnabled,
        RestrictNoteSyncinSalesloft,
        BaltoIntegrationEnable,
        LeadPoolSize,
        DialerDispositionsAdded,
        CreateNewListsEnabled,
        CsvListLoadingEnabled,
        UsePhoneNumberPriorityOptimization,
        CrmListReportFolders,
        SalesforceReportListsEnabled,
        ManagerCanDownloadLists,
        PitcherCanDownloadLists,
        UserAdminEnabled,
        BlindLeadPoolEnabled,
        LeadSourceAndStateFieldCheckEnabled,
        TimeZoneCheckEnabled,
        CountryCheckEnabled,
        TitlesCheckEnabled,
        ListFatigueCheck,
        AutoEnrichEnabled,
        NotificationSettingsEnabled,
        TimeZone,
        FollowUpCallsCheck,
        LeadInjectionEnabled,
        LeadInjectionTime,
        ConnectTaskFieldsEnabled,
        SubStatusEnabled,
        CrmShowAllReportFolders,
        NotificationSettings,
        MultiTouchEnabled,
        SmartListEnabled,
        CallFutureCallBackContacts,
        GracefulExitEnabled,
        BadNumberFiltering,
        WrongNumberFiltering,
        DncFiltering,
        CallRecordingFieldEnabled,
        AllowRepsToRateTheirOwnRecordings,
        RepsManagersExecSponsorsEmailGoalSettings,
        LoginNotificationSettings,
        LoginNotificationSettingsEnabled,
        EnrichOnIEEnabled,
        UpdateTaskStatusOnAttempt,
        LogoUrl,
        ActivityType,
        VSFEnabled,
        CsvLeadPoolEnabled,
        TaskAttemptsSyncEnabled,
        SecondarySubStatusEnabled,
        UpdateDialerTaskStatusOnAttempt,
        DualConsentRecordingsEnabled,
        ShowCountryNameOnConnectPopUp,
        HotTransferEnabled,
        EnableExtendedFilter,
        SalesforcePersonAccountEnabled,
        AppointmentInviteEmail,
        AppointmentInvitePassword,
        AppointmentInviteBody,
        FeedbackEmailBody,
        AppointmentInviteSubject,
        AppointmentInviteLocation,
        Accountobject,
        Prospectobject,
        TurnOffUpdateDueDate,
        TurnOffTemplateListValidation,
        WeeklyEmailAlert,
        OODEnabled,
        GoalSettings,
        ActOnEmail,
        HotTransferCallerID,
        RCLeadEmail,
        RepEmailIds,
        ReminderFromEmail,
        SendInvitationToProspect,
        SyncAgentDispositionEnabled,
        SessionScheduling,
        AttemptsThreshold,
        ConversationsThreshold,
        AppointmentInviteServiceProvider,
        CASLogs,
        WebsyncWebsocketEnabled,
        UseProbableDirectNumberEnabled,
        ConnectOnHelloEnabled,
        CallAfterTimeEnabled,
        CallOnDaysEnabled,
        PBRuleEnabled,
        MaxAttemptsRestricted,
        SchedulerInviteEmail,
        SchedulerInvitePassword,
        ActivityReportEmailAlert,
        VaronisStatusFieldSettings,
        ContactDisqualifiedStatus,
        LeadDisqualifiedStatus,
        ContactDisqualifiedSubStatus,
        LeadDisqualifiedSubStatus,
        AsteriskQueuesEnabled,
        OODNotificationEnabled,
        AttemptCounterSyncEnabled,
        AttemptDetailsSyncEnabled,
        ActivitySummaryReps,
        ActivitySummaryManagers,
        ConversionAlertManagersEmail,
        FollowUpCallListsCrm,
        PreferredAgentEnabled,
        AutoDetectVoiceMailForDirectNumber,
        PostRequestEnabled,
        ClickToDialSetting,
        ScoreCardReportManagers,
        IsTeamsEnabled,
        GateKeeperBlock,
        NegativeDispositionFiltering,
        LocalTouch,
        BhRestToken,
        BhRestUrl,
        CrmLinkOpenEnabled,
        TaskTypeSyncOnAttemptEnabled,
        SyncTaskReminderEnabled,
        TaskTypeEnabled,
        MostRecentCampaignFilter,
        EnableAutoRefreshListForCompany,
        DisableListRefresh,
        IsScorecardEnabled,
        ProductListEnabled,
        SyncTaskRelatedToFieldEnabled,
        CSVCompanyOnly,
        BlockPastDatedContacts,
        SendEmailOnCallPopupEnabled,
        TaskRecordingsUrlSyncEnabled,
        OpportunityWonStage,
        EnableHomeDashboard,
        IsPersonAccountEnabled,
        RepLockerEnabled,
        IsFollowUpDateOptional,
        EnableFollowUpEmail,
        TransferFollowUpContact,
        AutoLogOffSettingEnabled,
        AutoLogOffTime,
        AutoLogOffTimeAndExcludedRepIds,
        AgentCallAutoDisposeOnCallFailure,
        ZeroTalkTimeCallAutomation,
        DisableAutomation,
        PrioritizeTransferPredictedLeads,
        AttemptThrottlingUsingMl,
        SyncCampaignNameForCompletedTask,
        ActOnEmailEnabledForMultiTouch,
        AllowMultipleVM,
        CloseOpenTasks,
        RealTimeConversationEnabled,
        RealTimeAlertReps,
        RealTimeDispositions,
        RealTimeAlertManagers,
        RealTimeProblemDispositions,
        SyncCampaignIdToTask,
        CompanyIVRAutomationEnabled,
        DisableRecording,
        EnableDialerCallForLeadInjection,
        StopRecordingsAtHotTransfer,
        AllowPPforReps,
        PositiveConversationsThreshold,
        HourlyBreakdownActivityReportEnabled,
        NoPositiveConversationNotificationEnabled,
        MaxRatingScale,
        ShowTimeZoneInGMT,
        ShowGracefulExitInActivityReport,
        FTPCredentials,
        RestrictedAttemptsEnabled,
        MaxCallPerContact,
        AttributionReportEnabled,
        OpportunityStageNames,
        KNLeadEmail,
        NoOfDaysRecordingAvailable,
        LeadCustomValue,
        ContactCustomValue,
        DisqualifyDuplicateContactsEnabled,
        UseDefaultStatusesWhenStatusNotMappedDisabled,
        ShowLastConnectDispositionForCSV,
        DoNotUpdateLeadOwnerEnable,
        AutomaticTimeZoneDetectionEnabled,
        BusinessStartHours,
        BusinessEndhours,
        LunchTimeZoneDetectionEnabled,
        BusinessLunchStartHours,
        BusinessLunchEndhours,
        EnableAutoClearingDNC,
        PreserveLocalTouch,
        SearchLinkOnPopUp,
        LeadPoolCheck,
        DisableDownloadRecordingUsers,
        DisableStreamingRecordingUsers,
        DisableRecordingUsers,
        DownloadListAsSingleFile,
        ManagerCanEditLeadPoolSize,
        SfTaskTitlesEnabled,
        CompanyUrlDisplayLabel,
        AllowMultipleCSVFollowUpList,
        AllowCustomFieldsForCSV,
        MandatoryLeadSourceEnabled,
        DefaultLeadSourceForLead,
        DefaultLeadSourceForContact,
        JoyIntegrationEnable,
        UseOptmizedCodeForCallLists,
        DisableTaskCreationForProspectInSugar,
        callOnDaysEnabled,
        ArchiveListEnabled,
        DisableVoicemail,
        ConditionlFormattingEnabled,
        SyncUserFacingDisposition,
        EnableMassEditCallLists,
        AgentDispostitionSyncEnabledForOutreach,
        EnhanceWriteBackEnabled,
        EnhanceWriteBackInterval,
        EnableCSVDataLoader,
        FollowUpSequenceEnabledForOutreach,
        DisableStageForOutreach,
        CustomAgentDispositionsEnabled,
        TargetSessionDialTime,
        TargetConversations,
        IsDailyAutoScoreCard,
        IsWeeklyAutoScoreCard,
        IsMonthlyAutoScoreCard,
        CSVLeadFeedEmail,
        CSVLeadFeedEnabled,
        CSVLeadFeedListId,
        ValidPhoneNumbersEnabledForOutreach,
        RealTimeConversationTeamEnabled,
        TeamId,
        DispositionsId,
        EmailId,
        TaskOutreachNotesEnabled,
        OutreachDispositionSyncEnabled,
        AutoCreateOutreachSequenceList,
        ContactInformationCustomField,
        RemoveCallMeFeature,
        AllowAnonymousDownloadRecording,
        EnableUpdateDisposition,
        ExecutiveSummaryAlertEnabled,
        ExecutiveSummaryManagers,
        RandomizedLeadPool,
        EnableCompanytoRestrictCallingonEmergencyAreas,
        CompanyStateofEmergency,
        AttemptSetting,
        ConversationSetting,
        MeetingSetting,
        SessionDialTimeSetting,
        ShowZerosageUserForScorCardUser,
        ExtendedCallPopUpEnabled,
        EnableSentimentForSalesloft,
        EnableSentimentForSalesloftAttempts,
        DisableStatusForSalesloft,
        EnableEmailFlowinPP,
        UserManagementSettingCount,
        FollowUpCadenceEnabledForSalesloft,
        DropVoicemailToTargetVm,
        ShowOnlyLightingSessionDataInScorecard,
        EditContactOnPopupEnabled,
        CustomDispStatusEnabledForBH,
        ScorecardCompanyAccountOwner,
        ScorecardAccountOwnerMailList,
        ScorecardCSM,
        DefaultReferralContactStatus,
        EnableDefaultLeadContactStatusforreferral,
        DefaultReferralLeadStatus,
        ExcludeRepsFromAutoLogOff,
        ProdsHoursStartTime,
        ProdsHoursEndTime,
        ProdsHoursStartAndEndTime,
        AgentRingingEnabledForClient,
        Enableringingforagents,
        StepNotesEnabled,
        CollectionEnabledForOutreach,
        PenaltyBoxReportSettingsEnabled,
        PenaltyBoxReportManagers,
        DailyPenaltyBoxReportSettingsEnabled,
        WeeklyPenaltyBoxReportSettingsEnabled,
        MaxCallAttemptsPerDay,
        GlobalDailyJobTriggerSetting,
        GlobalDailyJobStatusSetting,
        ProspectNotesEnabledForOutreach,
        ShowRepGoalFunctionalitySetting,
        MandatoryListPriority,
        TriggersEnabled,
        ConversationHistoryReportSettingsEnabled,
        ConversationHistoryReportManagers,
        EnableCallPreferenceForLeadPool,
        ReferralPhoneFieldOptionalForSalesloft,
        ReferralPhoneFieldValueSalesloft,
        CompanyLevelDualConsentSetting,
        EnableAutoRefresh,
        AutoRefreshSec,
        EnableUserManagement,
        IdpName,
        SignInURL,
        Issuer,
        X509Certificate,
        OktaIDPEnabled,
        DisableSignInForAllUsers,
        NumberOfAttempts,
        NumberOfConversations,
        NoAttemptsNotificationEnabled,
        NoConversationsNotificationEnabled,
        AttemptRepLocker,
        ConversationsRepLocker,
        ConversationsWithoutConversionRepLocker,
        AttemptsWithoutConversionRepLocker,
        ConversationsWithoutPositiveDispositionRepLocker,
        NoAttemptWithoutConversionNotificationEnabled,
        NoConversationWithoutConversionNotificationEnable,
        SFConsumerKey,
        SFConsumerSecret,
        OutreachFlowEnabled,
        CollectionsEnabledForNextGen,
        ConnectedNumberSyncEnabled,
        PollingForOutreachEnabled,
        SalesloftFlowEnabled,
        FollowUpQuickListCollection,
        CollectionsForNextGen,
        DisableSFAttemptsForNextGen,
        OpportunityTypes,
        DisableRecordingDownloadFromJob,
        DefaultSalesloftAttemptSentiment,
        DefaultSalesloftAttemptDisposition,
        DisableUserNotAvailableAttemptsForNextGen,
        DisableInsightsWritebackForNextGen,
        EnhancedCallDocumentationUsers,
        CallPopupVersion,
        RepInitiatedRecording,
        EnableEnhancedCallPopUp,
        EnableMobileCalendarInvite,
        StandardSFAttemptsNextGenEnabled,
        NextGenAttemptsForNonConversationEnabled,
        BaltoToken,
        ConnectedNumberLogicEnabled,
        EnablePollingForFollowUps,
        TimeZoneForOutreachTasks,
        DisplayCommentsOnNextSteps,
        EnableSequenceEnrollmentForProblemAttempt,
        SSOTokenURL,
        SSOClientID,
        SSOClientSecret,
        SSORedirectUrl,
        SPInitiatedSSOEnabled,
        SSOAuthorizationURL,
        SSOProvider,
        AllowNumberToDial,
        NextGenEnabledUsers,
        isFutpMapped,
        PingTokenURL,
        PingClientID,
        PingClientSecret,
        PingRedirectUrl,
        PingIdEnabled,
        PingAuthorizationURL,
        DuplicateContactCheckEnabled,
        EnableRedisCacheflowForListLoading,
        ClientID,
        ClientSecret,
        CrmUrl
    }

}
