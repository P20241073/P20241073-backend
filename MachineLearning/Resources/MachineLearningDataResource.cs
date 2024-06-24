using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class InputItem
{
    [Required]
    public int gender { get; set; }

    [Required]
    public int age { get; set; }

    [Required]
    public int @class { get; set; }

    [Required]
    public int average_day_smartphone_usage_duration { get; set; }

    [Required]
    public int social_media_usage { get; set; }

    [Required]
    public int most_used_app_type { get; set; }

    [Required]
    public int sas_1 { get; set; }

    [Required]
    public int sas_2 { get; set; }

    [Required]
    public int sas_3 { get; set; }

    [Required]
    public int sas_4 { get; set; }

    [Required]
    public int sas_5 { get; set; }

    [Required]
    public int sas_6 { get; set; }

    [Required]
    public int sas_7 { get; set; }

    [Required]
    public int sas_8 { get; set; }

    [Required]
    public int sas_9 { get; set; }

    [Required]
    public int sas_10 { get; set; }
    [Required]
    public int total { get; set; }
}

public class Inputs
{
    [Required]
    public List<InputItem> input1 { get; set; }
}

public class MachineLearningDataResource
{
    [Required]
    public Inputs Inputs { get; set; }
}