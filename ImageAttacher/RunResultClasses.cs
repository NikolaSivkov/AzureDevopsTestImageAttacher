using System;
using System.Collections.Generic;
using System.Text;

namespace ImageAttacher.RunResultClasses
{

    public class RunResults
    {
        public int count { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public Project project { get; set; }
        public DateTime startedDate { get; set; }
        public DateTime completedDate { get; set; }
        public float durationInMs { get; set; }
        public string outcome { get; set; }
        public int revision { get; set; }
        public string state { get; set; }
        public Testcase testCase { get; set; }
        public Testrun testRun { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public int priority { get; set; }
        public Build build { get; set; }
        public string errorMessage { get; set; }
        public DateTime createdDate { get; set; }
        public string url { get; set; }
        public string failureType { get; set; }
        public string automatedTestStorage { get; set; }
        public string automatedTestType { get; set; }
        public string testCaseTitle { get; set; }
        public string stackTrace { get; set; }
        public object[] customFields { get; set; }
        public Failingsince failingSince { get; set; }
        public Releasereference releaseReference { get; set; }
        public int testCaseReferenceId { get; set; }
        public Runby runBy { get; set; }
        public Lastupdatedby lastUpdatedBy { get; set; }
        public string automatedTestName { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Testcase
    {
        public string name { get; set; }
    }

    public class Testrun
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Build
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Failingsince
    {
        public DateTime date { get; set; }
        public Build1 build { get; set; }
        public Release release { get; set; }
    }

    public class Build1
    {
        public int id { get; set; }
        public int definitionId { get; set; }
        public string branchName { get; set; }
    }

    public class Release
    {
        public int id { get; set; }
        public string name { get; set; }
        public int environmentId { get; set; }
        public object environmentName { get; set; }
        public int definitionId { get; set; }
        public int environmentDefinitionId { get; set; }
        public object environmentDefinitionName { get; set; }
    }

    public class Releasereference
    {
        public int id { get; set; }
        public string name { get; set; }
        public int environmentId { get; set; }
        public object environmentName { get; set; }
        public int definitionId { get; set; }
        public int environmentDefinitionId { get; set; }
        public object environmentDefinitionName { get; set; }
    }

    public class Runby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links
    {
        public Avatar avatar { get; set; }
    }

    public class Avatar
    {
        public string href { get; set; }
    }

    public class Lastupdatedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links1 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links1
    {
        public Avatar1 avatar { get; set; }
    }

    public class Avatar1
    {
        public string href { get; set; }
    }

}
