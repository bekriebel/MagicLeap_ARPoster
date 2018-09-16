using UnityEngine;

namespace MagicLeap
{
    /// <summary>
    /// This class is responsible for creating and moving the video player system
    /// </summary>
    public class VideoSystemLauncher : MonoBehaviour
    {
        #region Private Variables
        [SerializeField, Tooltip("Position offset of the object relative to Reference Transform")]
        private Vector3 _positionOffset;

        [Header("Video Player System")]
        [SerializeField, Tooltip("Video Player System")]
        private GameObject _videoSystemObject;
        private Vector3 _videoSystemVel;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Validates input variables
        /// </summary>
        void Awake()
        {
            if (null == _videoSystemObject)
            {
                Debug.LogError("VideoSystemLauncher._videoSystemObject not set, disabling script.");
                enabled = false;
                return;
            }
        }

        /// <summary>
        /// Enables the video player system
        /// </summary>
        void OnEnable()
        {
            UpdatePositionRotation();
            _videoSystemObject.SetActive(true);
        }

        /// <summary>
        /// Disables the video player system instance
        /// </summary>
        void OnDisable()
        {
            {
                _videoSystemObject.SetActive(false);
            }
        }

        /// <summary>
        /// Update video player system position
        /// </summary>
        void Update()
        {
            UpdatePositionRotation();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Calculate and return the position which the video player system should look at
        /// </summary>
        /// <returns>The absolute position of the new target</returns>
        private Vector3 GetPosition()
        {
            return transform.position + transform.TransformDirection(_positionOffset);
        }

        /// <summary>
        /// Update the position of the object to match the reference image
        /// </summary>
        void UpdatePositionRotation()
        {
            Vector3 position = GetPosition();

            // Update video player system position
            _videoSystemObject.transform.position = Vector3.SmoothDamp(_videoSystemObject.transform.position, position, ref _videoSystemVel, 1.0f);
            _videoSystemObject.transform.rotation = transform.rotation;
        }
        #endregion
    }
}
