namespace backend_tcc.lib.Class
{
    public class ValidationRulesResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesResult"/> class.
        /// </summary>
        public ValidationRulesResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesResult"/> class.
        /// </summary>
        /// <param name="memeberName">Name of the memeber.</param>
        /// <param name="message">The message.</param>
        public ValidationRulesResult(string memeberName, string message)
        {
            this.MemberName = memeberName;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesResult"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationRulesResult(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.  May be null for general validation issues.
        /// </value>
        public string MemberName { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
    }
}
