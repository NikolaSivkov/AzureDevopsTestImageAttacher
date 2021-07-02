using System;
using System.Collections.Generic;
using System.Text;

namespace ImageAttacher.Runs
{
    public class TestRuns
    {
        public Value[] value { get; set; }
        public int count { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Buildconfiguration buildConfiguration { get; set; }
        public bool isAutomated { get; set; }
        public Owner owner { get; set; }
        public Project1 project { get; set; }
        public DateTime startedDate { get; set; }
        public DateTime completedDate { get; set; }
        public string state { get; set; }
        public int totalTests { get; set; }
        public int incompleteTests { get; set; }
        public int notApplicableTests { get; set; }
        public int passedTests { get; set; }
        public int unanalyzedTests { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public Lastupdatedby lastUpdatedBy { get; set; }
        public int revision { get; set; }
        public Release release { get; set; }
        public Runstatistic[] runStatistics { get; set; }
        public string webAccessUrl { get; set; }
        public Pipelinereference pipelineReference { get; set; }
    }

    public class Buildconfiguration
    {
        public int id { get; set; }
        public string number { get; set; }
        public string flavor { get; set; }
        public string platform { get; set; }
        public int buildDefinitionId { get; set; }
        public Project project { get; set; }
    }

    public class Project
    {
        public string name { get; set; }
    }

    public class Owner
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

    public class Project1
    {
        public string id { get; set; }
        public string name { get; set; }
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

    public class Release
    {
        public int id { get; set; }
        public object name { get; set; }
        public int environmentId { get; set; }
        public object environmentName { get; set; }
        public int definitionId { get; set; }
        public int environmentDefinitionId { get; set; }
        public object environmentDefinitionName { get; set; }
    }

    public class Pipelinereference
    {
        public int pipelineId { get; set; }
        public Stagereference stageReference { get; set; }
        public Phasereference phaseReference { get; set; }
        public Jobreference jobReference { get; set; }
    }

    public class Stagereference
    {
    }

    public class Phasereference
    {
    }

    public class Jobreference
    {
    }

    public class Runstatistic
    {
        public string state { get; set; }
        public string outcome { get; set; }
        public int count { get; set; }
    }
}