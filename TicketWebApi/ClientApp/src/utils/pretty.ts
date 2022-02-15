class Pretty {
  prettyDate(value: Date | null | undefined) {
    if (!value) return "";
    const dateTime = new Date(value);
    if (!dateTime) return "";
    return dateTime.toLocaleDateString();
  }

  prettyTime(value: Date | null | undefined) {
    if (!value) return "";
    const dateTime = new Date(value);
    if (!dateTime) return "";
    return dateTime.toLocaleTimeString();
  }

  prettyDateTime(value: Date | null | undefined) {
    if (!value) return "";
    const dateTime = new Date(value);
    if (!dateTime) return "";
    const result = this.prettyDate(dateTime) + " " + this.prettyTime(dateTime);
    return result.trim();
  }

  prettyStudentName(lastName?: string | null, firstName?: string | null, middleName?: string | null, nameSuffix?: string | null) {
    return lastName?.trim() + ", " + firstName?.trim() + (middleName?.trim() ? " " + middleName?.trim() : "") + (nameSuffix?.trim() ? " " + nameSuffix.trim() : "");
  }
}

export const pretty = new Pretty();
