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
        [SerializeField, Tooltip("Prefab of the Video Player System")]
        private GameObject _videoSystemPrefab;
        private Transform _videoSystemInstance;
        private Vector3 _videoSystemVel;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Validates input variables
        /// </summary>
        void Awake()
        {
            if (null == _videoSystemPrefab)
            {
                Debug.LogError("VideoSystemLauncher._videoSystemPrefab not set, disabling script.");
                enabled = false;
                return;
            }
        }

        /// <summary>
        /// Creates an instance of the video player system
        /// </summary>
        void OnEnable()
        {
            _videoSystemInstance = Instantiate(_videoSystemPrefab.transform, GetPosition(), transform.rotation);
            _videoSystemInstance.gameObject.SetActive(true);
        }

        /// <summary>
        /// Destroys the video player system instance
        /// </summary>
        void OnDisable()
        {
            if (null != _videoSystemInstance)
            {
                Destroy(_videoSystemInstance.gameObject, 1.1f);
                _videoSystemInstance = null;
            }
        }

        /// <summary>
        /// Update video player system position
        /// </summary>
        void Update()
        {
            Vector3 position = GetPosition();

            // Update video player system position
            _videoSystemInstance.position = Vector3.SmoothDamp(_videoSystemInstance.position, position, ref _videoSystemVel, 1.0f);
            _videoSystemInstance.rotation = transform.rotation;
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
        #endregion
    }
}