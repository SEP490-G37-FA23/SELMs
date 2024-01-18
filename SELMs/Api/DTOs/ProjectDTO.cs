using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SELMs.Api.DTOs
{
	public class ProjectDTO
	{
		public int project_id { get; set; }

		[Required]
		public string project_code { get; set; }

		[Required]
		public string project_name { get; set; }

		[Required]
		public string acronym { get; set; }

		public string description { get; set; }

		[Required]
		public string manager { get; set; }


		[Required]
		public string start_date { get; set; }


		[Required, DateGreaterThan("start_date", ErrorMessage = "End date must be greater than the start date.")]
		public string end_date { get; set; }


		[Required, DataType(DataType.DateTime)]
		public DateTime? create_date { get; set; }

		[Required]
		public string creater { get; set; }

		[Required]
		public bool status { get; set; }

		[Required]
		public Nullable<int> location_id { get; set; }
	}










	public class DateGreaterThanAttribute : ValidationAttribute
	{
		private readonly string dependentPropertyName;

		public DateGreaterThanAttribute(string dependentPropertyName)
		{
			this.dependentPropertyName = dependentPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var property = validationContext.ObjectType.GetProperty(dependentPropertyName);

			if (property == null)
				throw new ArgumentException("Property with this name not found");


			string startDateString = property.GetValue(validationContext.ObjectInstance) as string;


			DateTime startDate = DateTime.ParseExact(startDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			DateTime endDate = DateTime.ParseExact(value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);


			if (startDate >= endDate)
				return new ValidationResult(ErrorMessage);

			return ValidationResult.Success;
		}
	}
}